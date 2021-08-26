using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartyStreets.InternationalStreetApi;
using Xunit;

using SmartyStreetsTests.Service;
using Lookup = SmartyStreets.USStreetApi.Lookup;

namespace SmartyStreetsTests
{
    public class SmartyStreetsServiceTests
    {

        private ISmartyStreetsService SmartyStreetsService { get; }

        public SmartyStreetsServiceTests()
        {
            var configuration = GetConfiguration();

            var serviceCollection = new ServiceCollection()
                .AddSingleton<ISmartyStreetsService, SmartyStreetsService>(sp => new SmartyStreetsService(configuration));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            SmartyStreetsService = serviceProvider.GetService<ISmartyStreetsService>();
        }

        [Fact]
        public async void SearchPlacesAsync_UsingEnhancedMatch_Fail()
        {
            var streetAddress= new StreetDto()
            {
                City = "BALTIMORE",
                State = "MD",
                Street = "1 ROSEDALE",
                Street2 = null,
                ZipCode = "21229",
                MatchStrategy = Lookup.ENHANCED
            };

            var resultUsingComponent = await SmartyStreetsService.SearchPlacesAsync(streetAddress);

            Assert.NotNull(resultUsingComponent.Analysis.Footnotes); //should return L#E#I#
            Assert.NotNull(resultUsingComponent.Analysis.DpvFootnotes);

        }

        [Fact]
        public async void SearchPlacesAsync_UsingStrictMatch_Pass()
        {
            var streetAddress = new StreetDto()
            {
                City = "BALTIMORE",
                State = "MD",
                Street = "1 ROSEDALE",
                Street2 = null,
                ZipCode = "21229",
                MatchStrategy = Lookup.STRICT
            };

            var resultUsingComponent = await SmartyStreetsService.SearchPlacesAsync(streetAddress);

            Assert.NotNull(resultUsingComponent.Analysis.Footnotes);
            Assert.NotNull(resultUsingComponent.Analysis.DpvFootnotes);

        }


        public static IConfiguration GetConfiguration()
        {
            var root = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", true)
                .Build();

            return root;
        }
    }
}
