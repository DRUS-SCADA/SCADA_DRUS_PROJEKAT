using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [ServiceContract(CallbackContract = typeof(ITrendingCallback))]
    public interface ITrending
    {
        [OperationContract]
        void SubscriberInitialization();
    }
}
