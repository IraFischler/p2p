using p2p.Entities.Info;
using p2p.Entities.File;
using p2p.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace p2pWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string loginUser(string xmlCOntent);

        [OperationContract]
        string registerUser(UserRegisterDTO urd);

        [OperationContract]
        string signoutUser(string xmlContent);

        [OperationContract]
        string enableDisableUser(UserInfoDTO uid);

        [OperationContract]
        UsersListDTO getUsersList();

        [OperationContract]
        string deleteUser(UserInfoDTO uid);

        [OperationContract]
        string updateUser(UserInfoDTO uid);

        [OperationContract]
        FileSearchResultDTO searchFiles(string xmlContent);

        [OperationContract]
        FilesListDTO getFilesList();

        [OperationContract]
        FileResponseDTO downloadRequest(FileRequestDTO request);

        [OperationContract]
        StatisticsDTO getStatistics();


    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "p2pWcf.ContractType".
   
}
