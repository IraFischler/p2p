using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p2p.Entities;
using p2p.Entities.User;
using p2p.Entities.Admin;
using p2p.Entities.File;
using p2p.Entities.Info;
using System.Windows;

namespace p2pWcf.DAL
{
    public class p2pSqlDAL
    {

        #region USER

        public static string signoutUser(UserLoginDTO uld)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.userName == uld.UserName &&
                              u.password == uld.UserName
                              select u;

                    if (res.Count() == 1)
                    {
                        var us = res.SingleOrDefault();
                        var res2 = (from f in ctx.Files
                                    where f.userId == uld.Id
                                    select f).ToList().SingleOrDefault();

                        us.connected = false;
                        ctx.Files.DeleteOnSubmit(res2);

                        ctx.SubmitChanges();
                        return "OK";
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }

            return "ERROR";
        }

        public static string enableDisableUser(UserInfoDTO uid)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.Id == uid.Id
                              select u;
                    if (res.Count() == 1)
                    {
                        var user = res.SingleOrDefault();
                        user.enabled = uid.Enabled;

                        ctx.SubmitChanges();
                        return "OK";
                    }
                    else
                    {
                        if (res.Count() == 0)
                        {
                            return "no match found";
                        }
                        if (res.Count() > 1)
                        {
                            return "more than 1 match found";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            return "ERROR";
        }

        public static string updateUser(UserInfoDTO uid)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.Id == uid.Id
                              select u;

                    if (res.Count() == 1)
                    {
                        var u = res.Single();

                        u.userName = uid.UserName;
                        u.password = uid.Password;
                        u.email = uid.Email;

                        ctx.SubmitChanges();
                        return "OK";
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            return "ERROR";
        }

        public static string deleteUser(UserInfoDTO uid)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.Id == uid.Id
                              select u;

                    if (res.Count() == 1)
                    {
                        var us = res.Single();
                        var res2 = (from f in ctx.Files
                                    where f.userId == uid.Id
                                    select f).ToList().SingleOrDefault();

                        ctx.Files.DeleteOnSubmit(res2);
                        ctx.Users.DeleteOnSubmit(us);

                        ctx.SubmitChanges();
                        return "OK";
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }

            return "ERROR";
        }

        //Login any type of user: admin, user ...
        public static string loginUser(UserLoginDTO uld)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {

                    var res = from u in ctx.Users
                              where u.userName == uld.UserName &&
                              u.password == uld.Password
                              select u;
                    if (res.Count() == 1)
                    {
                        User us = res.Single();
                        us.downloadPath = uld.DownloadPath;
                        us.uploadPath = uld.UploadPath;
                        us.ip = uld.Ip;
                        us.port = uld.Port;
                        us.loginDate = DateTime.Now;
                        uld.Id = us.Id;
                        us.connected = true;

                        foreach (var f in uld.Files)
                        {
                            File file = new File()
                            {
                                name = f.FileName,
                                size = f.FileSize,
                                type = f.FileType,
                                updateDate = DateTime.Now,
                                userId = us.Id
                            };
                            ctx.Files.InsertOnSubmit(file);
                        }
                        ctx.SubmitChanges();
                        return "OK";
                    }
                    else
                    {
                        if (res.Count() == 0)
                        {
                            return "no match found";
                        }
                        if (res.Count() > 1)
                        {
                            return "more than 1 match found";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            return "ERROR";
        }

        public static UsersListDTO getUsersList()
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = (from u in ctx.Users
                               select new UserInfoDTO()
                               {
                                   Id = (int)u.Id,
                                   UserName = u.userName,
                                   Password = u.password,
                                   Email = u.email
                               });

                    if (res.Count() > 0)
                    {
                        return new UsersListDTO()
                        {
                            Users = res.ToList(),
                            SearchResult = "OK"
                        };
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            return new UsersListDTO()
            {
                SearchResult = "ERROR"
            };
        }

        public static string registerUser(UserRegisterDTO urd)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.userName == urd.UserName &&
                              u.password == urd.Password && u.email == urd.Email
                              select u;
                    if (res.Count() == 0)
                    {
                        User u = new User()
                        {
                            userName = urd.UserName,
                            password = urd.Password,
                            email = urd.Email,
                            enabled = true
                        };

                        ctx.Users.InsertOnSubmit(u);
                        ctx.SubmitChanges();
                        return "OK";
                    }
                    return "user already exist";
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");

            }
            return "ERROR";
        }

        //If the user signout update the DB that he isnt available 
        //public static void signoutUser(UserSignoutDTO usd) { }

        #endregion

        #region FILE
        public static FileSearchResultDTO searchFiles(FileSearchDTO fsd)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.userName == fsd.UserName &&
                              u.password == fsd.Password
                              select u;
                    if (res.Count() == 1)
                    {
                        var res2 = (from f in ctx.Files
                                    where f.name.Equals(fsd.SearchText)
                                    group f by new { f.name, f.size, f.type } into g

                                    select new FileInfoDTO()
                                    {
                                        FileName = g.Key.name,
                                        FileSize = g.Key.size,
                                        FileType = g.Key.type,
                                        NumOfUsers = g.Count()
                                    }).ToList();

                        return new FileSearchResultDTO()
                        {
                            Files = res2,
                            SearchResult = "OK"
                        };
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            return new FileSearchResultDTO()
            {
                SearchResult = "ERROR"
            };
        }

        public static FileResponseDTO downloadRequest(FileRequestDTO frd)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = from u in ctx.Users
                              where u.userName == frd.UserName &&
                              u.password == frd.Password
                              select u;
                    if (res.Count() == 1)
                    {
                        var res2 = (from fl in ctx.Files
                                    join u in ctx.Users
                                    on fl.userId equals u.Id
                                    where fl.name == frd.FileName
                                    select new FileResponseDTO
                                    {
                                        Ip = u.ip,
                                        Port = (int)u.port
                                    }).ToList().Take(1).SingleOrDefault();

                        return res2;
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            return new FileResponseDTO();

        }

        public static FilesListDTO getFilesList()
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = (from f in ctx.Files
                               select new FileInfoDTO()
                               {
                                   Id = (int)f.Id,
                                   FileName = f.name,
                                   FileSize = f.size,
                                   FileType = f.type
                               });

                    if (res.Count() > 0)
                    {
                        return new FilesListDTO()
                        {
                            Files = res.ToList(),
                            SearchResult = "OK"
                        };
                    }
                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }

            return new FilesListDTO()
            {
                SearchResult = "OK"
            };

        }

        #endregion

        public static void addLog(string method, string message, string additionalData)
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    //annonimos constarctor
                    Log l = new p2pWcf.Log()
                    {
                        method = method,
                        message = message,
                        additionalData = additionalData,
                        updateDate = DateTime.Now
                    };

                    //insert into table
                    ctx.Logs.InsertOnSubmit(l);
                    ctx.SubmitChanges();
                }

            }
            catch (Exception)
            {

            }
        }

        public static StatisticsDTO getStatistics()
        {
            try
            {
                using (p2pDataContext ctx = new p2pDataContext())
                {
                    var res = (from u in ctx.Users
                               select u).ToList();

                    var res1 = (from u in ctx.Users
                                select u.enabled).ToList();

                    var res2 = (from f in ctx.Files
                                select f).ToList();

                    StatisticsDTO sd = new StatisticsDTO()
                    {
                        NumOfUsers = res.Count(),
                        NumOfActiveUsers = res1.Count(),
                        NumOfFiles = res2.Count()
                    };

                    return sd;

                }
            }
            catch (Exception e)
            {
                // get method name using reflection
                addLog(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, "");
            }
            StatisticsDTO sd1 = new StatisticsDTO()
            {
                NumOfUsers = 0,
                NumOfActiveUsers = 0,
                NumOfFiles = 0
            };
            return sd1;

        }
        //The admin can create user 
        public static void adminCreateUser(AdminCreateUserDTO acud) { }

        //The admin can create user 
        public static void adminDeleteUser(AdminDeleteUserDTO adud) { }

        //The admin is able to block/ unblock user and update the user status 
        public static bool adminDisableEnableUser(AdminDisableEnableUserDTO adeu) { return false; }

        //Only the admin able to update user info
        public static bool adminUpdateUserInfo(AdminUpdateUserInfoDTO auuid) { return false; }




    }




}

