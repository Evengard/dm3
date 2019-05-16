using System.Collections.Generic;

namespace DM.Services.MessageQueuing.Configuration
{
    /// <summary>
    /// Consumer configuration
    /// </summary>
    public class MessageConsumeConfiguration
    {
        /// <summary>
        /// Consumer queue
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Source exchange
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// Binding routes for queue
        /// </summary>
        public IEnumerable<string> RoutingKeys { get; set; }

        /// <summary>
        /// Queue arguments
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; }

        /// <summary>
        /// Queue exclusiveness flag
        /// </summary>
        public bool Exclusive { get; set; }
    }
}