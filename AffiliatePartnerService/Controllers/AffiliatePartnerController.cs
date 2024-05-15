using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AffiliatePartnerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliatePartnerController : ControllerBase
    {
        private readonly IAffiliatePartnerBusinessProcessor _logic;
        private readonly ILogger<AffiliatePartnerController> _logger;

        public AffiliatePartnerController(IAffiliatePartnerBusinessProcessor logic, ILogger<AffiliatePartnerController> logger)
        {
            _logic = logic;
            _logger = logger;
        }


        // GET: api/Experts
        [HttpGet]
        public async Task<object> Get()
        {
            _logger.LogInformation("Fetching Affiliate Partners Data..");
            var affiliatePartners = await _logic.Get();
            return Ok(affiliatePartners);
        }

        /// <summary>
        /// Get an Experts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET : api/Experts/1
        /// </remarks>
        /// <param name="Id"></param>
        [HttpGet("{Id}", Name = "Get")]
        public async Task<ActionResult<AffiliatePartnerReadDTO>> Get(Guid Id)
        {
            _logger.LogInformation("Fetching Affiliate Partners details for Id : " + Id.ToString());
            var affiliatePartners = await _logic.Get(Id);
            return affiliatePartners != null ? (ActionResult<AffiliatePartnerReadDTO>)Ok(affiliatePartners) : NotFound();
        }

        [HttpPost]
        public async Task<object> Post(AffiliatePartnerCreateDTO affiliatePartnerCreateDTO)
        {
            var response = await _logic.Post(affiliatePartnerCreateDTO);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("{Id:guid}")]
        public async Task<object> Put(Guid Id, AffiliatePartnerCreateDTO affiliatePartnerCreateDTO)
        {
            var response = await _logic.Put(Id, affiliatePartnerCreateDTO);

            if (response.IsSuccess)
            {
                Guid guid = (Guid)response.Data.GetType().GetProperty("Id").GetValue(response.Data);

                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch]
        public async Task<object> Patch(Guid Id, [FromBody] JsonPatchDocument<AffiliatePartnerCreateDTO> affiliatePartnerDtoPatch)
        {
            var response = await _logic.Patch(Id, affiliatePartnerDtoPatch);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var affiliatePartners = await _logic.Delete(Id);
            return affiliatePartners != null ? Ok(affiliatePartners) : NotFound();
        }


        [HttpGet("GenerateReferralLink/{Id}", Name = "GenerateReferralLink")]
        public async Task<ActionResult<AffiliatePartnerReadDTO>> GenerateReferralLink(Guid Id)
        {
            _logger.LogInformation("Fetching Affiliate Partners Referral Link details for Id : " + Id.ToString());
            var affiliatePartners = await _logic.GenerateReferralLink(Id);
            return affiliatePartners != null ? (ActionResult<AffiliatePartnerReadDTO>)Ok(affiliatePartners) : NotFound();
        }

    }
}
