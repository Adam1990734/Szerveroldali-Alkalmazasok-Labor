using EstateFlow.Server.Contracts.Meters;

namespace EstateFlow.Server.Services
{
    public interface IMeterReadingService
    {
        Task<MeterReadingSubmissionResult> SubmitReadingAsync(Guid meterId, SubmitMeterReadingRequest request);
    }
}
