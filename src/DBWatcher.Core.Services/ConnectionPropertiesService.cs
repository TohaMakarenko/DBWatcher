using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Services
{
    public class ConnectionPropertiesService : IConnectionPropertiesService
    {
        private readonly ICryptoManager _cryptoManager;
        private readonly IUnitOfWork _work;

        public ConnectionPropertiesService(IUnitOfWork work,
            ICryptoManager cryptoManager)
        {
            _work = work;
            _cryptoManager = cryptoManager;
        }

        public Task SaveConnectionProperty(ConnectionProperties connectionProperties)
        {
            if (connectionProperties.IsPasswordEncrypted)
                connectionProperties.Password = _cryptoManager.Encrypt(connectionProperties.Password);

            return _work.ConnectionPropertiesRepository.Insert(connectionProperties);
        }

        public Task<ConnectionProperties> GetById(int id)
        {
            return _work.ConnectionPropertiesRepository.Get(id);
        }

        public async Task<ConnectionProperties> GetByIdDecrypted(int id)
        {
            var props = await _work.ConnectionPropertiesRepository.Get(id);
            if (props.IsPasswordEncrypted) props.Password = _cryptoManager.Decrypt(props.Password);

            return props;
        }
    }
}