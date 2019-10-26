using System;
using System.Collections.Concurrent;
using In.Cqrs.Nats.Abstract;
using NATS.Client;

namespace In.Cqrs.Nats
{
    public class NatsConnectionFactory : INatsConnectionFactory
    {
        private readonly INatsSerializer _serializer;
        private readonly Options _options;

        private ConcurrentDictionary<Type, IEncodedConnection> _connections =
            new ConcurrentDictionary<Type, IEncodedConnection>();

        public NatsConnectionFactory(INatsSerializer serializer, Options options)
        {
            _serializer = serializer;
            _options = options;
        }

        public IEncodedConnection Get<T>()
        {
            var connection = GetConnection<T>();
            connection.OnDeserialize = _serializer.Deserialize<T>;
            connection.OnSerialize = _serializer.Serialize<T>;

            return connection;
        }

        public void Dispose()
        {
            foreach (var connection in _connections)
            {
                connection.Value.Dispose();
            }
        }

        private IEncodedConnection GetConnection<T>()
        {
            var type = typeof(T);

            if (_connections.TryGetValue(type, out var connection))
                return connection;

            try
            {
                connection = new ConnectionFactory().CreateEncodedConnection(_options);
            }
            catch (NATSConnectionException ex)
            {
                throw new Exception($"Nats connection error: {ex.Message}");
            }
            catch (NATSNoServersException ex)
            {
                throw new Exception($"Nats no server error: {ex.Message}");
            }

            while (!_connections.TryAdd(type, connection)) ;

            return connection;
        }
    }
}