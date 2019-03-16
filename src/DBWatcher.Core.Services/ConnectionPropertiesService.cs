using System;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core.Services
{
    public class ConnectionPropertiesService : IConnectionPropertiesService
    {
        private readonly IConnectionPropertiesRepository _connectionPropertiesRepository;
        private readonly ICryptoManager _cryptoManager;

        public ConnectionPropertiesService(IConnectionPropertiesRepository connectionPropertiesRepository,
            ICryptoManager cryptoManager)
        {
            _connectionPropertiesRepository = connectionPropertiesRepository;
            _cryptoManager = cryptoManager;
        }

        public Task SaveConnectionProperty(ConnectionProperties connectionProperties)
        {
            if (connectionProperties.IsPasswordEncrypted) {
                connectionProperties.Password = _cryptoManager.Encrypt(connectionProperties.Password);
            }

            return _connectionPropertiesRepository.Insert(connectionProperties);
        }

        public Task<ConnectionProperties> GetById(Guid id)
        {
            return _connectionPropertiesRepository.Get(id);
        }

        public async Task<ConnectionProperties> GetByIdDecrypted(Guid id)
        {
            var props = await _connectionPropertiesRepository.Get(id);
            if (props.IsPasswordEncrypted) {
                props.Password = _cryptoManager.Decrypt(props.Password);
            }

            return props;
        }
    }
}