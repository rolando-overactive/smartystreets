using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace SmartyStreetsTests.Service
{

    public interface ISmartyStreetsService
    {
        public Task<Candidate> SearchPlacesAsync(StreetDto propertyAddress);
        public Task<Candidate> SearchPlacesAsync(string address);

    }

    public class SmartyStreetsService : ISmartyStreetsService
    {

        private IConfiguration Configuration { get; set; }

        public SmartyStreetsService(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<Candidate> SearchPlacesAsync(string address)
        {
            var lookup = new Lookup
            {
                Street = address,
                MaxCandidates = 1,
                MatchStrategy = Lookup.STRICT
            };
            return await Search(lookup);
        }


        public async Task<Candidate> SearchPlacesAsync(StreetDto propertyAddress)
        {
            var lookup = new Lookup
            {
                Street = propertyAddress.Street,
                State = propertyAddress.State,
                City = propertyAddress.City,
                ZipCode = propertyAddress.ZipCode,
                Street2 = propertyAddress.Street2, // Check if we can rename this property
                MaxCandidates = 1,
                MatchStrategy = Lookup.STRICT
            };
            return await Search(lookup);
        }

        private async Task<Candidate> Search(Lookup lookup)
        {
            var client = new ClientBuilder(Configuration["AppSettings:SmartyStreetsApiAuthID"], Configuration["AppSettings:SmartyStreetsApiAuthToken"]).WithLicense(new List<string> { "us-core-cloud" })
                .BuildUsStreetApiClient();
            try
            {
                client.Send(lookup);
                var candidate = lookup.Result.FirstOrDefault();

                if (candidate == null)
                    throw new Exception($"SmartyStreet API request failed.\nPhrase: address input does not match with any SmartyStreet address");

                return await Task.FromResult(candidate);
            }
            catch (SmartyException ex)
            {
                throw new Exception($"SmartyStreet API request failed.\nPhrase: {ex.Message}" );
            }
        }
    }
}
