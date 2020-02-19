using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GLvNextApi.Models;
using System.Net.Http;
using System.Text.Json;

namespace GLvNextApi
{
    public class RemoteOfferWorker : BackgroundService
    {
        private readonly ILogger<RemoteOfferWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _clientFactory;

        public RemoteOfferWorker(ILogger<RemoteOfferWorker> logger, IServiceScopeFactory scopeFactory, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _clientFactory = clientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TODO: refresh offers - first need to add repeatable Id (hash of key?) to offers that won't collide with locally added offers
            //so that we can keep track of them.
            
            //Just run on startup for now.
            //while (!stoppingToken.IsCancellationRequested)
            //{
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await DoWork();
                //await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            //}
        }

        /// <summary>
        /// Get offers from API and convert to local offer format. 
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task DoWork()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OfferContext>();
                var httpClient = _clientFactory.CreateClient("remoteOffers");
                var request = new HttpRequestMessage(HttpMethod.Get,"offers");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var offersRoot = await JsonSerializer.DeserializeAsync<RemoteOfferRoot>(responseStream);
                    //TODO check if there is a way to do this more async
                    await Task.Run(()=>context.Offers.AddRange(offersRoot.data.Select(remoteOffer => new Offer(remoteOffer))));
                    await context.SaveChangesAsync();
                }
                else
                {
                    //TODO add error logging here if time allows
                }
            }
        } 
    }
}
