using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IServiceContract
    {
        [OperationContract]
        void StartSession();

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void PushSample(DataContract data);

        [OperationContract]
        void EndSession();
    }
}
