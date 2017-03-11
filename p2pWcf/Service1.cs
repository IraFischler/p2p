using p2p.Entities;
using p2p.Entities.File;
using p2p.Entities.User;
using p2p.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace p2pWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        public string loginUser(string xmlContent)
        {
            //deserilization
            Type _type =  typeof(UserLoginDTO);
            UserLoginDTO user = (UserLoginDTO)(p2p.Utils.XmlFormatter.GetObjectFromXML(xmlContent, _type));
            return DAL.p2pSqlDAL.loginUser(user);
        }

        public string signoutUser(string xmlContent)
        {
            Type _type = typeof(UserLoginDTO);
            UserLoginDTO user = (UserLoginDTO)(p2p.Utils.XmlFormatter.GetObjectFromXML(xmlContent, _type));
            return DAL.p2pSqlDAL.signoutUser(user);
        }

        public string registerUser(UserRegisterDTO urd)
        {
            return DAL.p2pSqlDAL.registerUser(urd);
        }

        public string enableDisableUser(UserInfoDTO uid)
        {
            return DAL.p2pSqlDAL.enableDisableUser(uid);
        }

        public UsersListDTO getUsersList()
        {
            return DAL.p2pSqlDAL.getUsersList();
        }

        public string deleteUser(UserInfoDTO uid)
        {
            return DAL.p2pSqlDAL.deleteUser(uid);
        }

        public string updateUser(UserInfoDTO uid)
        {
           return DAL.p2pSqlDAL.updateUser(uid);
        }

        public  FileSearchResultDTO searchFiles(string xmlContent)
        {
            Type _type = typeof(FileSearchDTO);
            FileSearchDTO search = (FileSearchDTO)(p2p.Utils.XmlFormatter.GetObjectFromXML(xmlContent, _type));
            return DAL.p2pSqlDAL.searchFiles(search);
        }

        //add download file func/////////////////////////////////////////////////
        public FileResponseDTO downloadRequest(FileRequestDTO request) // FileResponseDTO change FileDownloadResponseDTO
        {
            return DAL.p2pSqlDAL.downloadRequest(request);
        }

        //register function will get an objedt DTO
        public string registerUser(string xmlContent)
        {
            //deserilization
            Type _type = typeof(UserRegisterDTO);
            UserRegisterDTO user = (UserRegisterDTO)(p2p.Utils.XmlFormatter.GetObjectFromXML(xmlContent, _type));
            return DAL.p2pSqlDAL.registerUser(user);
        }

        public StatisticsDTO getStatistics()
        {
            return DAL.p2pSqlDAL.getStatistics();
        }

        public FilesListDTO getFilesList()
        {
           return DAL.p2pSqlDAL.getFilesList();
        }
    }
}
