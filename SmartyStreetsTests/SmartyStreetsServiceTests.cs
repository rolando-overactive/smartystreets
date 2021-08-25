using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using SmartyStreetsTests.Service;

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
        public async void SearchPlacesAsync_()
        {
            var resultUsingComponent = await SmartyStreetsService.SearchPlacesAsync(new StreetDto()
            {
                City = "Baltimore",
                State = "MD",
                Street = "1 Rosedale",
                Street2 = string.Empty,
                ZipCode = "21229"

            });

            var resultUsingFreeForm = await SmartyStreetsService.SearchPlacesAsync("1 Rosedale Baltimore MD 21229");

           Assert.NotNull(resultUsingFreeForm);
           Assert.NotNull(resultUsingComponent);

           Assert.Equal(resultUsingFreeForm.Analysis.DpvMatchCode, resultUsingComponent.Analysis.DpvMatchCode);
           Assert.Equal(resultUsingFreeForm.Analysis.Footnotes, resultUsingComponent.Analysis.Footnotes);
           Assert.Equal(resultUsingFreeForm.Analysis.DpvFootnotes, resultUsingComponent.Analysis.DpvFootnotes);


           var (isValid, validationMessages) = resultUsingFreeForm.Analysis.ToValidationResult();
           
            Assert.False(isValid);

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
