using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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


            Assert.NotNull(resultUsingComponent.Analysis.Footnotes);
            Assert.Equal("L#E#I#", resultUsingComponent.Analysis.Footnotes);
            Assert.NotNull(resultUsingComponent.Analysis.DpvFootnotes);
            Assert.Equal("AABB", resultUsingComponent.Analysis.DpvFootnotes);

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
            Assert.Equal("L#E#I#", resultUsingComponent.Analysis.Footnotes);
            Assert.NotNull(resultUsingComponent.Analysis.DpvFootnotes);
            Assert.Equal("AABB", resultUsingComponent.Analysis.DpvFootnotes);

            var (isValid, validationMessages) = resultUsingComponent.Analysis.ToValidationResult();
        }

       

        [Fact]
        public async void SearchPlacesAsync_UsingEnhancedMatch_Fail1()
        {
            var streetAddress = new StreetDto()
            {
                City = "Gulliver",
                State = "Michigan",
                Street = "W Gulliver Lake Rd",
                Street2 = null,
                ZipCode = "49840",
                MatchStrategy = Lookup.ENHANCED
            };

            var resultUsingComponent = await SmartyStreetsService.SearchPlacesAsync(streetAddress);
            var (isValid, validationMessages) = resultUsingComponent.Analysis.ToValidationResult();     
        }



        /// <summary>
        /// Expected result are from https://www.smartystreets.com/products/single-address
        /// </summary>
        [Fact]
        public async void SearchPlacesAsync_ReturnLatitudAndLongitute_Fail()
        {
            var streetAddress = new StreetDto()
            {
                City = "RIVIERA BEACH",
                State = "FL",
                Street = "1221 BIMINI LN",
                Street2 = null,
                ZipCode = "33404",
                MatchStrategy = Lookup.STRICT
            };

            var resultUsingComponent = await SmartyStreetsService.SearchPlacesAsync(streetAddress);
            Assert.NotNull(resultUsingComponent);
            Assert.Equal(-80.037132, resultUsingComponent.Metadata.Longitude );
            Assert.Equal(26.792206, resultUsingComponent.Metadata.Latitude);

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
