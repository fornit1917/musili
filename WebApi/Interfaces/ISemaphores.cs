using System.Threading;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface ISemaphores {
         SemaphoreSlim GetSemaphoreForService(TracksSourceService service);
    }
}