using ItunesApi.Domain.Model;

namespace ItunesApi.Application.BusinesLogic.IBusinessLogic
{
    public interface ISongsRockBL
    {
        public Task<Rootobject.Songs> GetSongsRockCall();
    }
}
