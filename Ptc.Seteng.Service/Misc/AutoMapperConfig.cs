using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Ptc.AspnetMvc.Authentication;


namespace Ptc.Seteng.Misc
{
    public static class AutoMapperConfig
    {
        public static void Init(Autofac.IContainer container)
        {

            Mapper.Initialize(x =>
            {
                x.ConstructServicesUsing(t => container.Resolve(t));
                x.AddProfile<TcmpdatProfile>();
                x.AddProfile<TcmpdatReverseProfile>();
                x.AddProfile<TvenderProfile>();
                x.AddProfile<TvenderReverseProfile>();
                x.AddProfile<TcallogProfile>();
                x.AddProfile<TcallogReverseProfile>();
                x.AddProfile<TCallLogDateRecordProfile>();
                x.AddProfile<TCallLogDateRecordReverseProfile>();
                x.AddProfile<TCALINVProfile>();
                x.AddProfile<TCALINVReverseProfile>();
                x.AddProfile<MobileCallogSearchProfile>();
                x.AddProfile<TusrmstProfile>();
                x.AddProfile<TusrmstReverseProfile>();
                x.AddProfile<TusrdtlProfile>();
                x.AddProfile<TusrdtlReverseProfile>();
                x.AddProfile<RolesProfile>();
                x.AddProfile<UserProfile>();
                x.AddProfile<RoleAuthProfile>();
                x.AddProfile<TvenderTechnicianProfile>();
                x.AddProfile<TvenderTechnicianReverseProfile>();
                x.AddProfile<TtechnicianGroupProfile>();
                x.AddProfile<TtechnicianGroupReverseProfile>();
                x.AddProfile<TTechnicianGroupClaimsProfile>();
                x.AddProfile<TacceptLogProfile>();
                x.AddProfile<TcallogRecordProfile>();
                x.AddProfile<TcallogRecordReverseProfile>();
                x.AddProfile<TrefdatProfile>();
                x.AddProfile<TUSRVENRELATIONProfile>();
                x.AddProfile<TUSRVENRELATIONReverseProfile>();
                x.AddProfile<TVNDZOProfile>();
                x.AddProfile<TVNDZOReverseProfile>();
                x.AddProfile<TZOCODEProfile>();
                x.AddProfile<TZOCODEReverseProfile>();
                x.AddProfile<TCallogCourseProfile>();
                x.AddProfile<TCallogCourseReverseProfile>();
                x.AddProfile<TASSETSProfile>();
                x.AddProfile<TDAMMTNProfile>();
                x.AddProfile<TFINISHProfile>();
                x.AddProfile<TWRKKNDProfile>();
                x.AddProfile<MobileCallogNoFinishProfile>();
                x.AddProfile<MobileCallogNoFinishReverseProfile>();
                x.AddProfile<TstsdatProfile>();
                x.AddProfile<TstrmstProfile>();
            });
        }

        #region 公司主檔

        private class TcmpdatProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TCMPDAT, Tcmpdat>()
                        .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                        .ForMember(dest => dest.CompName, opt => opt.MapFrom(src => src.Comp_Name))
                        .ForMember(dest => dest.CompShort, opt => opt.MapFrom(src => src.Comp_Short))
                        .ForMember(dest => dest.CompAddr, opt => opt.MapFrom(src => src.Comp_Addr))
                        .ForMember(dest => dest.EmailAccount, opt => opt.MapFrom(src => src.Email_Account))
                        .ForMember(dest => dest.SortSeq, opt => opt.MapFrom(src => src.Sort_Seq))
                        .ForMember(dest => dest.ChkStrNm, opt => opt.MapFrom(src => src.Chk_Str_Nm))
                        .ForMember(dest => dest.CompSts, opt => opt.MapFrom(src => src.Comp_Sts))
                        .ForMember(dest => dest.CompImage, opt => opt.MapFrom(src => src.Comp_Image))
                        .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                        .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_Date))
                        .IgnoreAllNonExisting();
            }
        }

        private class TcmpdatReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tcmpdat, DataBase.TCMPDAT>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Comp_Name, opt => opt.MapFrom(src => src.CompName))
                     .ForMember(dest => dest.Comp_Short, opt => opt.MapFrom(src => src.CompShort))
                     .ForMember(dest => dest.Comp_Addr, opt => opt.MapFrom(src => src.CompAddr))
                     .ForMember(dest => dest.Email_Account, opt => opt.MapFrom(src => src.EmailAccount))
                     .ForMember(dest => dest.Sort_Seq, opt => opt.MapFrom(src => src.SortSeq))
                     .ForMember(dest => dest.Chk_Str_Nm, opt => opt.MapFrom(src => src.ChkStrNm))
                     .ForMember(dest => dest.Comp_Sts, opt => opt.MapFrom(src => src.CompSts))
                     .ForMember(dest => dest.Comp_Image, opt => opt.MapFrom(src => src.CompImage))
                     .ForMember(dest => dest.Update_User, opt => opt.MapFrom(src => src.UpdateUser))
                     .ForMember(dest => dest.Update_Date, opt => opt.MapFrom(src => src.UpdateDate))
                     .IgnoreAllNonExisting();
            }
        }



        #endregion


        #region 權限相關

        private class RolesProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TSYSROL, Tsysrol>()
                     .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role_Name))
                     .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_Date))
                     .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                     .ForMember(dest => dest.PageAuth, opt => opt.MapFrom(src => src.PageAuth))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }

        private class RoleAuthProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TSYSROL, RoleAuth>()
                   .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                   .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role_Name))
                   .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_Id))
                   .ForMember(dest => dest.PageAuth, opt => opt.MapFrom(src =>
                       string.IsNullOrEmpty(src.PageAuth) == true ? null : JsonConvert.DeserializeObject<List<AuthItem>>(src.PageAuth)))
                   .IgnoreAllNonExisting().ReverseMap();
            }
        }

        private class UserProfile : Profile
        {
            protected override void Configure()
            {

                CreateMap<DataBase.TUSRMST, UserBase>()
                   .ForMember(dest => dest.RoleAuth, opt => opt.MapFrom(src => src.TSYSROL))
                   .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.TUSRDTL.Pass_Wd))
                   .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                   .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_Id))
                   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User_Name))
                   .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_Id))
                   .ForMember(dest => dest.ZoCd, opt => opt.MapFrom(src => src.Z_O))
                   .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Id))
                   .ForMember(dest => dest.VenderName, opt => opt.MapFrom(src => src.Vender_Name))
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email_Account))
                   .ForMember(dest => dest.TelNo, opt => opt.MapFrom(src => src.Tel_No))
                   .ForMember(dest => dest.IdSts, opt => opt.MapFrom(src => src.Id_Sts.Equals("Y")))
                   .ForMember(dest => dest.PageAuth, opt => opt.MapFrom(src =>
                      string.IsNullOrEmpty(src.PageAuth) == true ? null : JsonConvert.DeserializeObject<List<AuthItem>>(src.PageAuth)))
                          .IgnoreAllNonExisting().ReverseMap();


                CreateMap<DataBase.TVenderTechnician, UserBase>()
                 .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                 .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Account))
                 .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                 .IgnoreAllNonExisting().ReverseMap();

            }


            private static List<AuthItem> convertPageAuth(string userstrPageAuth, string rolestrPageAuth)
            {
                List<AuthItem> userPageAuth =
                    JsonConvert.DeserializeObject<List<AuthItem>>(userstrPageAuth ?? "[]");
                List<AuthItem> PageAuth =
                    JsonConvert.DeserializeObject<List<AuthItem>>(rolestrPageAuth ?? "[]");

                foreach (var item in userPageAuth ?? new List<AuthItem>())
                {
                    if (item.isDeny)
                    {
                        //黑名單
                        PageAuth.RemoveAll(x => x.GroupName == item.GroupName);
                    }
                    else
                    {
                        //白名單
                        //判斷角色是否擁有相同功能 有的話以白名單權限為主
                        if (PageAuth.FindIndex(x => x.GroupName == item.GroupName) >= 0)
                            PageAuth.Find(x => x.GroupName == item.GroupName).AuthType = item.AuthType;
                        else
                            PageAuth.Add(new AuthItem() { GroupName = item.GroupName, AuthType = item.AuthType });
                    }
                }

                return PageAuth;
            }

        }



        #endregion

        #region 廠商主檔

        private class TvenderProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TVENDER, Tvender>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                     .ForMember(dest => dest.VenderName, opt => opt.MapFrom(src => src.Vender_Name))
                     .ForMember(dest => dest.VenderAddr, opt => opt.MapFrom(src => src.Vender_Addr))
                     .ForMember(dest => dest.VenderId, opt => opt.MapFrom(src => src.Vender_Id))
                     .ForMember(dest => dest.TaxType, opt => opt.MapFrom(src => src.Tax_Type))
                     .ForMember(dest => dest.ContactWindow, opt => opt.MapFrom(src => src.Contact_Window))
                     .ForMember(dest => dest.TelNo, opt => opt.MapFrom(src => src.Tel_No))
                     .ForMember(dest => dest.FaxNo, opt => opt.MapFrom(src => src.Fax_No))
                     .ForMember(dest => dest.EmailAccount, opt => opt.MapFrom(src => src.Email_Account))
                     .ForMember(dest => dest.HolidayTelNo, opt => opt.MapFrom(src => src.Holiday_Tel_No))
                     .ForMember(dest => dest.EmcTelNo, opt => opt.MapFrom(src => src.Emc_Tel_No))
                     .ForMember(dest => dest.ShowNewCall, opt => opt.MapFrom(src => src.Show_New_Call))
                     .ForMember(dest => dest.IsDataTrans, opt => opt.MapFrom(src => src.Is_DataTrans))
                     .ForMember(dest => dest.IsPush, opt => opt.MapFrom(src => src.Is_Push))
                     .ForMember(dest => dest.TransPara, opt => opt.MapFrom(src => src.Trans_Para))
                     .ForMember(dest => dest.Affiliates, opt => opt.MapFrom(src => src.Affiliates))
                     .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.Close_Date))
                     .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                     .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_Date))
                     .ForMember(dest => dest.TTechnicianGroup, opt => opt.MapFrom(src => src.TTechnicianGroup))
                     .ForMember(dest => dest.TVenderTechnician, opt => opt.MapFrom(src => src.TVenderTechnician))
                     .IgnoreAllNonExisting();
            }
        }

        private class TvenderReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tvender, DataBase.TVENDER>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.VenderCd))
                     .ForMember(dest => dest.Vender_Name, opt => opt.MapFrom(src => src.VenderName))
                     .ForMember(dest => dest.Vender_Addr, opt => opt.MapFrom(src => src.VenderAddr))
                     .ForMember(dest => dest.Vender_Id, opt => opt.MapFrom(src => src.VenderId))
                     .ForMember(dest => dest.Tax_Type, opt => opt.MapFrom(src => src.TaxType))
                     .ForMember(dest => dest.Contact_Window, opt => opt.MapFrom(src => src.ContactWindow))
                     .ForMember(dest => dest.Tel_No, opt => opt.MapFrom(src => src.TelNo))
                     .ForMember(dest => dest.Fax_No, opt => opt.MapFrom(src => src.FaxNo))
                     .ForMember(dest => dest.Email_Account, opt => opt.MapFrom(src => src.EmailAccount))
                     .ForMember(dest => dest.Holiday_Tel_No, opt => opt.MapFrom(src => src.HolidayTelNo))
                     .ForMember(dest => dest.Emc_Tel_No, opt => opt.MapFrom(src => src.EmcTelNo))
                     .ForMember(dest => dest.Show_New_Call, opt => opt.MapFrom(src => src.ShowNewCall))
                     .ForMember(dest => dest.Is_DataTrans, opt => opt.MapFrom(src => src.IsDataTrans))
                     .ForMember(dest => dest.Is_Push, opt => opt.MapFrom(src => src.IsPush))
                     .ForMember(dest => dest.Trans_Para, opt => opt.MapFrom(src => src.TransPara))
                     .ForMember(dest => dest.Affiliates, opt => opt.MapFrom(src => src.Affiliates))
                     .ForMember(dest => dest.Close_Date, opt => opt.MapFrom(src => src.CloseDate))
                     .ForMember(dest => dest.Update_User, opt => opt.MapFrom(src => src.UpdateUser))
                     .ForMember(dest => dest.Update_Date, opt => opt.MapFrom(src => src.UpdateDate))
                     .ForMember(dest => dest.TTechnicianGroup, opt => opt.MapFrom(src => src.TTechnicianGroup))
                     .ForMember(dest => dest.TVenderTechnician, opt => opt.MapFrom(src => src.TVenderTechnician))
                     .IgnoreAllNonExisting();
            }
        }



        #endregion

        #region 技師主檔

        private class TvenderTechnicianProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TVenderTechnician, TvenderTechnician>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.IsVendor, opt => opt.MapFrom(src => src.IsVendor))
                     .ForMember(dest => dest.DeviceID, opt => opt.MapFrom(src => src.DeviceID))
                     .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => src.Enable))
                     .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => src.LastLoginTime))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                     .ForMember(dest => dest.RegistrationID, opt => opt.MapFrom(src => src.RegistrationID))
                     .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                     .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                     .ForMember(dest => dest.TVENDER, opt => opt.MapFrom(src => src.TVENDER))
                     .ForMember(dest => dest.TTechnicianGroupClaims, opt => opt.MapFrom(src => src.TTechnicianGroupClaims))
                     .ForMember(dest => dest.TCALLOG, opt => opt.MapFrom(src => src.TCALLOG))
                     .IgnoreAllNonExisting();
            }
        }

        private class TvenderTechnicianReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TvenderTechnician, DataBase.TVenderTechnician>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.IsVendor, opt => opt.MapFrom(src => src.IsVendor))
                     .ForMember(dest => dest.DeviceID, opt => opt.MapFrom(src => src.DeviceID))
                     .ForMember(dest => dest.Enable, opt => opt.MapFrom(src => src.Enable))
                     .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                     .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => src.LastLoginTime))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                     .ForMember(dest => dest.RegistrationID, opt => opt.MapFrom(src => src.RegistrationID))
                     .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.VenderCd))
                     .ForMember(dest => dest.TVENDER, opt => opt.MapFrom(src => src.TVENDER))
                     .ForMember(dest => dest.TCALLOG, opt => opt.MapFrom(src => src.TCALLOG))
                     .ForMember(dest => dest.TTechnicianGroupClaims, opt => opt.MapFrom(src => src.TTechnicianGroupClaims))
                     .IgnoreAllNonExisting();
            }
        }



        #endregion

        #region 案件主檔


        private class TcallogProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TCALLOG, Tcallog>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.StoreCd, opt => opt.MapFrom(src => src.Store_Cd))
                     .ForMember(dest => dest.Zo, opt => opt.MapFrom(src => src.Z_O))
                     .ForMember(dest => dest.Do, opt => opt.MapFrom(src => src.D_O))
                     .ForMember(dest => dest.FiDate, opt => opt.MapFrom(src => src.Fi_Date))
                     .ForMember(dest => dest.CallerName, opt => opt.MapFrom(src => src.Caller_Name))
                     .ForMember(dest => dest.AstKind1, opt => opt.MapFrom(src => src.Ast_Kind1))
                     .ForMember(dest => dest.AstKind2, opt => opt.MapFrom(src => src.Ast_Kind2))
                     .ForMember(dest => dest.AstKind3, opt => opt.MapFrom(src => src.Ast_Kind3))
                     .ForMember(dest => dest.AssetCd, opt => opt.MapFrom(src => src.Asset_Cd))
                     .ForMember(dest => dest.AssetNo, opt => opt.MapFrom(src => src.Asset_No))
                     .ForMember(dest => dest.SapAssetNo, opt => opt.MapFrom(src => src.Sap_Asset_No))
                     .ForMember(dest => dest.DamageNo, opt => opt.MapFrom(src => src.Damage_No))
                     .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                     .ForMember(dest => dest.CallLevel, opt => opt.MapFrom(src => src.Call_Level))
                     .ForMember(dest => dest.CallDesc, opt => opt.MapFrom(src => src.Call_Desc))
                     .ForMember(dest => dest.RemarkAdd, opt => opt.MapFrom(src => src.Remark_Add))
                     .ForMember(dest => dest.FvDate, opt => opt.MapFrom(src => src.Fv_Date))
                     .ForMember(dest => dest.FdDate, opt => opt.MapFrom(src => src.Fd_Date))
                     .ForMember(dest => dest.FaDate, opt => opt.MapFrom(src => src.Fa_Date))
                     .ForMember(dest => dest.ArriveDate, opt => opt.MapFrom(src => src.Arrive_Date))
                     .ForMember(dest => dest.FcDate, opt => opt.MapFrom(src => src.Fc_Date))
                     .ForMember(dest => dest.FinishId, opt => opt.MapFrom(src => src.Finish_Id))
                     .ForMember(dest => dest.EngNo, opt => opt.MapFrom(src => src.Eng_No))
                     .ForMember(dest => dest.CloseSts, opt => opt.MapFrom(src => src.Close_Sts))
                     .ForMember(dest => dest.TacceptedLog, opt => opt.MapFrom(src => src.TAcceptedLog))
                     .ForMember(dest => dest.TvenderTechnician, opt => opt.MapFrom(src => src.TVenderTechnician))
                     .ForMember(dest => dest.TimePoint, opt => opt.MapFrom(src => src.TimePoint))
                     .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                     .ForMember(dest => dest.Updatedate, opt => opt.MapFrom(src => src.Update_Date))
                     .ForMember(dest => dest.WorkDesc, opt => opt.MapFrom(src => src.Work_Desc))
                     .ForMember(dest => dest.CoffeeCup, opt => opt.MapFrom(src => src.Coffee_Cup))
                     .ForMember(dest => dest.AppCloseDate, opt => opt.MapFrom(src => src.AppClose_Date))
                     .ForMember(dest => dest.VndEngId, opt => opt.MapFrom(src => src.VndEng_Id))
                     .ForMember(dest => dest.DamageProcNo, opt => opt.MapFrom(src => src.Damage_Proc_No))
                     .ForMember(dest => dest.TcallLogDateRecords, opt => opt.MapFrom(src => src.TCallLogDateRecord))
                     .IgnoreAllNonExisting();
            }
        }


        private class TcallogReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tcallog, DataBase.TCALLOG>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.Store_Cd, opt => opt.MapFrom(src => src.StoreCd))
                     .ForMember(dest => dest.Z_O, opt => opt.MapFrom(src => src.Zo))
                     .ForMember(dest => dest.D_O, opt => opt.MapFrom(src => src.Do))
                     .ForMember(dest => dest.Fi_Date, opt => opt.MapFrom(src => src.FiDate))
                     .ForMember(dest => dest.Caller_Name, opt => opt.MapFrom(src => src.CallerName))
                     .ForMember(dest => dest.Ast_Kind1, opt => opt.MapFrom(src => src.AstKind1))
                     .ForMember(dest => dest.Ast_Kind2, opt => opt.MapFrom(src => src.AstKind2))
                     .ForMember(dest => dest.Ast_Kind3, opt => opt.MapFrom(src => src.AstKind3))
                     .ForMember(dest => dest.Asset_Cd, opt => opt.MapFrom(src => src.AssetCd))
                     .ForMember(dest => dest.Asset_No, opt => opt.MapFrom(src => src.AssetNo))
                     .ForMember(dest => dest.Sap_Asset_No, opt => opt.MapFrom(src => src.SapAssetNo))
                     .ForMember(dest => dest.Damage_No, opt => opt.MapFrom(src => src.DamageNo))
                     .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.VenderCd))
                     .ForMember(dest => dest.Call_Level, opt => opt.MapFrom(src => src.CallLevel))
                     .ForMember(dest => dest.Call_Desc, opt => opt.MapFrom(src => src.CallDesc))
                     .ForMember(dest => dest.Remark_Add, opt => opt.MapFrom(src => src.RemarkAdd))
                     .ForMember(dest => dest.Fv_Date, opt => opt.MapFrom(src => src.FvDate))
                     .ForMember(dest => dest.Fd_Date, opt => opt.MapFrom(src => src.FdDate))
                     .ForMember(dest => dest.Fa_Date, opt => opt.MapFrom(src => src.FaDate))
                     .ForMember(dest => dest.Arrive_Date, opt => opt.MapFrom(src => src.ArriveDate))
                     .ForMember(dest => dest.Fc_Date, opt => opt.MapFrom(src => src.FcDate))
                     .ForMember(dest => dest.Finish_Id, opt => opt.MapFrom(src => src.FinishId))
                     .ForMember(dest => dest.Eng_No, opt => opt.MapFrom(src => src.EngNo))
                     .ForMember(dest => dest.Close_Sts, opt => opt.MapFrom(src => src.CloseSts))
                     .ForMember(dest => dest.TAcceptedLog, opt => opt.MapFrom(src => src.TacceptedLog))
                     .ForMember(dest => dest.TVenderTechnician, opt => opt.MapFrom(src => src.TvenderTechnician))
                     .ForMember(dest => dest.TimePoint, opt => opt.MapFrom(src => src.TimePoint))
                     .ForMember(dest => dest.Update_User, opt => opt.MapFrom(src => src.UpdateUser))
                     .ForMember(dest => dest.Update_Date, opt => opt.MapFrom(src => src.Updatedate))
                     .ForMember(dest => dest.Work_Desc, opt => opt.MapFrom(src => src.WorkDesc))
                     .ForMember(dest => dest.Coffee_Cup, opt => opt.MapFrom(src => src.CoffeeCup))
                     .ForMember(dest => dest.AppClose_Date, opt => opt.MapFrom(src => src.AppCloseDate))
                     .ForMember(dest => dest.VndEng_Id, opt => opt.MapFrom(src => src.VndEngId))
                     .ForMember(dest => dest.Damage_Proc_No, opt => opt.MapFrom(src => src.DamageProcNo))
                     .ForMember(dest => dest.TCallLogDateRecord, opt => opt.MapFrom(src => src.TcallLogDateRecords))
                     .IgnoreAllNonExisting();

            }
        }


        private class TCallLogDateRecordProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TCallLogDateRecord, TCallLogDateRecord>()
                     .ForMember(dest => dest.Seq, opt => opt.MapFrom(src => src.Seq))
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.SN, opt => opt.MapFrom(src => src.SN))
                     .ForMember(dest => dest.RecordType, opt => opt.MapFrom(src => src.RecordType))
                     .ForMember(dest => dest.RecordDate, opt => opt.MapFrom(src => src.RecordDate))
                     .ForMember(dest => dest.Create_User, opt => opt.MapFrom(src => src.Create_User))
                     .ForMember(dest => dest.Create_Date, opt => opt.MapFrom(src => src.Create_Date))
                     .IgnoreAllNonExisting();
            }
        }
        private class TCallLogDateRecordReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TCallLogDateRecord, DataBase.TCallLogDateRecord>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.SN, opt => opt.MapFrom(src => src.SN))
                     .ForMember(dest => dest.RecordType, opt => opt.MapFrom(src => src.RecordType))
                     .ForMember(dest => dest.RecordDate, opt => opt.MapFrom(src => src.RecordDate))
                     .ForMember(dest => dest.Create_User, opt => opt.MapFrom(src => src.Create_User))
                     .ForMember(dest => dest.Create_Date, opt => opt.MapFrom(src => src.Create_Date))
                     .ForMember(dest => dest.Seq, opt => opt.MapFrom(src => src.Seq))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }


        private class TCALINVProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TCALINV, TCALINV>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                    .ForMember(dest => dest.Debit_Kind, opt => opt.MapFrom(src => src.Debit_Kind))
                    .ForMember(dest => dest.Invoice_No, opt => opt.MapFrom(src => src.Invoice_No))
                    .ForMember(dest => dest.Invoice_Date, opt => opt.MapFrom(src => src.Invoice_Date))
                    .ForMember(dest => dest.Work_Id, opt => opt.MapFrom(src => src.Work_Id))
                    .ForMember(dest => dest.Pre_Amt, opt => opt.MapFrom(src => src.Pre_Amt))
                    .ForMember(dest => dest.Tot_Amt, opt => opt.MapFrom(src => src.Tot_Amt))
                    .ForMember(dest => dest.Acc_Type, opt => opt.MapFrom(src => src.Acc_Type))
                    .ForMember(dest => dest.Direct_Amt, opt => opt.MapFrom(src => src.Direct_Amt))
                    .ForMember(dest => dest.Join_Amt, opt => opt.MapFrom(src => src.Join_Amt))
                    .ForMember(dest => dest.Comp_Amt, opt => opt.MapFrom(src => src.Comp_Amt))
                    .ForMember(dest => dest.Trans_YM, opt => opt.MapFrom(src => src.Trans_YM))
                    .ForMember(dest => dest.Create_User_Id, opt => opt.MapFrom(src => src.Create_User_Id))
                    .ForMember(dest => dest.Update_User, opt => opt.MapFrom(src => src.Update_User))
                    .ForMember(dest => dest.Update_Date, opt => opt.MapFrom(src => src.Update_Date))
                    .IgnoreAllNonExisting();
            }
        }

        private class TCALINVReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TCALINV, DataBase.TCALINV>()
                    .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                    .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                    .ForMember(dest => dest.Debit_Kind, opt => opt.MapFrom(src => src.Debit_Kind))
                    .ForMember(dest => dest.Invoice_No, opt => opt.MapFrom(src => src.Invoice_No))
                    .ForMember(dest => dest.Invoice_Date, opt => opt.MapFrom(src => src.Invoice_Date))
                    .ForMember(dest => dest.Work_Id, opt => opt.MapFrom(src => src.Work_Id))
                    .ForMember(dest => dest.Pre_Amt, opt => opt.MapFrom(src => src.Pre_Amt))
                    .ForMember(dest => dest.Tot_Amt, opt => opt.MapFrom(src => src.Tot_Amt))
                    .ForMember(dest => dest.Acc_Type, opt => opt.MapFrom(src => src.Acc_Type))
                    .ForMember(dest => dest.Direct_Amt, opt => opt.MapFrom(src => src.Direct_Amt))
                    .ForMember(dest => dest.Join_Amt, opt => opt.MapFrom(src => src.Join_Amt))
                    .ForMember(dest => dest.Comp_Amt, opt => opt.MapFrom(src => src.Comp_Amt))
                    .ForMember(dest => dest.Trans_YM, opt => opt.MapFrom(src => src.Trans_YM))
                    .ForMember(dest => dest.Create_User_Id, opt => opt.MapFrom(src => src.Create_User_Id))
                    .ForMember(dest => dest.Update_User, opt => opt.MapFrom(src => src.Update_User))
                    .ForMember(dest => dest.Update_Date, opt => opt.MapFrom(src => src.Update_Date))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }


        #endregion

        #region 案件主檔(View)


        private class MobileCallogSearchProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.VW_MobileCallogSearch, MobileCallogSearch>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.StoreCd, opt => opt.MapFrom(src => src.Store_Cd))
                     .ForMember(dest => dest.Zo, opt => opt.MapFrom(src => src.Z_O))
                     .ForMember(dest => dest.Do, opt => opt.MapFrom(src => src.D_O))
                     .ForMember(dest => dest.FiDate, opt => opt.MapFrom(src => src.Fi_Date))
                     .ForMember(dest => dest.CallerName, opt => opt.MapFrom(src => src.Caller_Name))
                     .ForMember(dest => dest.AstKind1, opt => opt.MapFrom(src => src.Ast_Kind1))
                     .ForMember(dest => dest.AstKind2, opt => opt.MapFrom(src => src.Ast_Kind2))
                     .ForMember(dest => dest.AstKind3, opt => opt.MapFrom(src => src.Ast_Kind3))
                     .ForMember(dest => dest.AssetCd, opt => opt.MapFrom(src => src.Asset_Cd))
                     .ForMember(dest => dest.AssetNo, opt => opt.MapFrom(src => src.Asset_No))
                     .ForMember(dest => dest.SapAssetNo, opt => opt.MapFrom(src => src.Sap_Asset_No))
                     .ForMember(dest => dest.DamageNo, opt => opt.MapFrom(src => src.Damage_No))
                     .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                     .ForMember(dest => dest.VenderName, opt => opt.MapFrom(src => src.Vender_Name))
                     .ForMember(dest => dest.CallLevel, opt => opt.MapFrom(src => src.Call_Level))
                     .ForMember(dest => dest.CallDesc, opt => opt.MapFrom(src => src.Call_Desc))
                     .ForMember(dest => dest.RemarkAdd, opt => opt.MapFrom(src => src.Remark_Add))
                     .ForMember(dest => dest.FvDate, opt => opt.MapFrom(src => src.Fv_Date))
                     .ForMember(dest => dest.FdDate, opt => opt.MapFrom(src => src.Fd_Date))
                     .ForMember(dest => dest.FaDate, opt => opt.MapFrom(src => src.Fa_Date))
                     .ForMember(dest => dest.ArriveDate, opt => opt.MapFrom(src => src.Arrive_Date))
                     .ForMember(dest => dest.FcDate, opt => opt.MapFrom(src => src.Fc_Date))
                     .ForMember(dest => dest.FinishId, opt => opt.MapFrom(src => src.Finish_Id))
                     .ForMember(dest => dest.EngNo, opt => opt.MapFrom(src => src.Eng_No))
                     .ForMember(dest => dest.CloseSts, opt => opt.MapFrom(src => src.Close_Sts))
                     .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store_Name))
                     .ForMember(dest => dest.StoreAddr, opt => opt.MapFrom(src => src.Store_Addr))
                     .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset_Name))
                     .ForMember(dest => dest.DamageDesc, opt => opt.MapFrom(src => src.Damage_Desc))
                     .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                     .ForMember(dest => dest.AcceptSn, opt => opt.MapFrom(src => src.AcceptSn))
                     .ForMember(dest => dest.AcceptAccount, opt => opt.MapFrom(src => src.AcceptAccount))
                     .ForMember(dest => dest.Telno, opt => opt.MapFrom(src => src.Tel_No))
                     .ForMember(dest => dest.Timepoint, opt => opt.MapFrom(src => src.TimePoint))
                     .ForMember(dest => dest.Workdesc, opt => opt.MapFrom(src => src.Work_Desc))
                     .ForMember(dest => dest.Acceptname, opt => opt.MapFrom(src => src.Acceptname))
                     .ForMember(dest => dest.AcceptDatetime, opt => opt.MapFrom(src => src.AcceptDatetime))
                     .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.WorkName))
                     .ForMember(dest => dest.Finish_Name, opt => opt.MapFrom(src => src.Finish_Name))
                     .ForMember(dest => dest.Mtn_Desc, opt => opt.MapFrom(src => src.Mtn_Desc))
                     .ForMember(dest => dest.RcvDate, opt => opt.MapFrom(src => src.Rcv_Date))
                     .ForMember(dest => dest.CoffeeCup, opt => opt.MapFrom(src => src.Coffee_Cup))
                     .ForMember(dest => dest.AppCloseDate, opt => opt.MapFrom(src => src.AppClose_Date))
                     .ForMember(dest => dest.VndEngId, opt => opt.MapFrom(src => src.VndEng_Id))
                     .ForMember(dest => dest.Pre_Amt, opt => opt.MapFrom(src => src.Pre_Amt))
                     .IgnoreAllNonExisting();
            }
        }

        #endregion


        #region 未完修案件管理(View)
        private class MobileCallogNoFinishProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.VW_MobileCallogNoFinish, MobileCallogSearch>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.StoreCd, opt => opt.MapFrom(src => src.Store_Cd))
                     .ForMember(dest => dest.Zo, opt => opt.MapFrom(src => src.Z_O))
                     .ForMember(dest => dest.Do, opt => opt.MapFrom(src => src.D_O))
                     .ForMember(dest => dest.FiDate, opt => opt.MapFrom(src => src.Fi_Date))
                     .ForMember(dest => dest.CallerName, opt => opt.MapFrom(src => src.Caller_Name))
                     .ForMember(dest => dest.AstKind1, opt => opt.MapFrom(src => src.Ast_Kind1))
                     .ForMember(dest => dest.AstKind2, opt => opt.MapFrom(src => src.Ast_Kind2))
                     .ForMember(dest => dest.AstKind3, opt => opt.MapFrom(src => src.Ast_Kind3))
                     .ForMember(dest => dest.AssetCd, opt => opt.MapFrom(src => src.Asset_Cd))
                     .ForMember(dest => dest.AssetNo, opt => opt.MapFrom(src => src.Asset_No))
                     .ForMember(dest => dest.SapAssetNo, opt => opt.MapFrom(src => src.Sap_Asset_No))
                     .ForMember(dest => dest.DamageNo, opt => opt.MapFrom(src => src.Damage_No))
                     .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                     .ForMember(dest => dest.VenderName, opt => opt.MapFrom(src => src.Vender_Name))
                     .ForMember(dest => dest.CallLevel, opt => opt.MapFrom(src => src.Call_Level))
                     .ForMember(dest => dest.CallDesc, opt => opt.MapFrom(src => src.Call_Desc))
                     .ForMember(dest => dest.RemarkAdd, opt => opt.MapFrom(src => src.Remark_Add))
                     .ForMember(dest => dest.FvDate, opt => opt.MapFrom(src => src.Fv_Date))
                     .ForMember(dest => dest.FdDate, opt => opt.MapFrom(src => src.Fd_Date))
                     .ForMember(dest => dest.FaDate, opt => opt.MapFrom(src => src.Fa_Date))
                     .ForMember(dest => dest.ArriveDate, opt => opt.MapFrom(src => src.Arrive_Date))
                     .ForMember(dest => dest.FcDate, opt => opt.MapFrom(src => src.Fc_Date))
                     .ForMember(dest => dest.FinishId, opt => opt.MapFrom(src => src.Finish_Id))
                     .ForMember(dest => dest.EngNo, opt => opt.MapFrom(src => src.Eng_No))
                     .ForMember(dest => dest.CloseSts, opt => opt.MapFrom(src => src.Close_Sts))
                     .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store_Name))
                     .ForMember(dest => dest.StoreAddr, opt => opt.MapFrom(src => src.Store_Addr))
                     .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset_Name))
                     .ForMember(dest => dest.DamageDesc, opt => opt.MapFrom(src => src.Damage_Desc))
                     .ForMember(dest => dest.AcceptSn, opt => opt.MapFrom(src => src.AcceptSn))
                     .ForMember(dest => dest.AcceptAccount, opt => opt.MapFrom(src => src.AcceptAccount))
                     .ForMember(dest => dest.Telno, opt => opt.MapFrom(src => src.Tel_No))
                     .ForMember(dest => dest.Timepoint, opt => opt.MapFrom(src => src.TimePoint))
                     .ForMember(dest => dest.Workdesc, opt => opt.MapFrom(src => src.Work_Desc))
                     .ForMember(dest => dest.Acceptname, opt => opt.MapFrom(src => src.Acceptname))
                     .ForMember(dest => dest.AcceptDatetime, opt => opt.MapFrom(src => src.AcceptDatetime))
                     .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.WorkName))
                     .ForMember(dest => dest.Finish_Name, opt => opt.MapFrom(src => src.Finish_Name))
                     .ForMember(dest => dest.Mtn_Desc, opt => opt.MapFrom(src => src.Mtn_Desc))
                     .ForMember(dest => dest.RcvDate, opt => opt.MapFrom(src => src.Rcv_Date))
                     .ForMember(dest => dest.CoffeeCup, opt => opt.MapFrom(src => src.Coffee_Cup))
                     .ForMember(dest => dest.AppCloseDate, opt => opt.MapFrom(src => src.AppClose_Date))
                     .ForMember(dest => dest.VndEngId, opt => opt.MapFrom(src => src.VndEng_Id))
                     .ForMember(dest => dest.Pre_Amt, opt => opt.MapFrom(src => src.Pre_Amt))
                     .IgnoreAllNonExisting();
            }
        }

        private class MobileCallogNoFinishReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<MobileCallogSearch, DataBase.VW_MobileCallogNoFinish>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.Store_Cd, opt => opt.MapFrom(src => src.StoreCd))
                     .ForMember(dest => dest.Z_O, opt => opt.MapFrom(src => src.Zo))
                     .ForMember(dest => dest.D_O, opt => opt.MapFrom(src => src.Do))
                     .ForMember(dest => dest.Fi_Date, opt => opt.MapFrom(src => src.FiDate))
                     .ForMember(dest => dest.Caller_Name, opt => opt.MapFrom(src => src.CallerName))
                     .ForMember(dest => dest.Ast_Kind1, opt => opt.MapFrom(src => src.AstKind1))
                     .ForMember(dest => dest.Ast_Kind2, opt => opt.MapFrom(src => src.AstKind2))
                     .ForMember(dest => dest.Ast_Kind3, opt => opt.MapFrom(src => src.AstKind3))
                     .ForMember(dest => dest.Asset_Cd, opt => opt.MapFrom(src => src.AssetCd))
                     .ForMember(dest => dest.Asset_No, opt => opt.MapFrom(src => src.AssetNo))
                     .ForMember(dest => dest.Sap_Asset_No, opt => opt.MapFrom(src => src.SapAssetNo))
                     .ForMember(dest => dest.Damage_No, opt => opt.MapFrom(src => src.DamageNo))
                     .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.VenderCd))
                     .ForMember(dest => dest.Vender_Name, opt => opt.MapFrom(src => src.VenderName))
                     .ForMember(dest => dest.Call_Level, opt => opt.MapFrom(src => src.CallLevel))
                     .ForMember(dest => dest.Call_Desc, opt => opt.MapFrom(src => src.CallDesc))
                     .ForMember(dest => dest.Remark_Add, opt => opt.MapFrom(src => src.RemarkAdd))
                     .ForMember(dest => dest.Fv_Date, opt => opt.MapFrom(src => src.FvDate))
                     .ForMember(dest => dest.Fd_Date, opt => opt.MapFrom(src => src.FdDate))
                     .ForMember(dest => dest.Fa_Date, opt => opt.MapFrom(src => src.FaDate))
                     .ForMember(dest => dest.Arrive_Date, opt => opt.MapFrom(src => src.ArriveDate))
                     .ForMember(dest => dest.Fc_Date, opt => opt.MapFrom(src => src.FcDate))
                     .ForMember(dest => dest.Finish_Id, opt => opt.MapFrom(src => src.FinishId))
                     .ForMember(dest => dest.Eng_No, opt => opt.MapFrom(src => src.EngNo))
                     .ForMember(dest => dest.Close_Sts, opt => opt.MapFrom(src => src.CloseSts))
                     .ForMember(dest => dest.Store_Name, opt => opt.MapFrom(src => src.StoreName))
                     .ForMember(dest => dest.Store_Addr, opt => opt.MapFrom(src => src.StoreAddr))
                     .ForMember(dest => dest.Asset_Name, opt => opt.MapFrom(src => src.AssetName))
                     .ForMember(dest => dest.Damage_Desc, opt => opt.MapFrom(src => src.DamageDesc))
                     .ForMember(dest => dest.AcceptSn, opt => opt.MapFrom(src => src.AcceptSn))
                     .ForMember(dest => dest.AcceptAccount, opt => opt.MapFrom(src => src.AcceptAccount))
                     .ForMember(dest => dest.Tel_No, opt => opt.MapFrom(src => src.Telno))
                     .ForMember(dest => dest.TimePoint, opt => opt.MapFrom(src => src.Timepoint))
                     .ForMember(dest => dest.Work_Desc, opt => opt.MapFrom(src => src.Workdesc))
                     .ForMember(dest => dest.Acceptname, opt => opt.MapFrom(src => src.Acceptname))
                     .ForMember(dest => dest.AcceptDatetime, opt => opt.MapFrom(src => src.AcceptDatetime))
                     .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.WorkName))
                     .ForMember(dest => dest.Finish_Name, opt => opt.MapFrom(src => src.Finish_Name))
                     .ForMember(dest => dest.Mtn_Desc, opt => opt.MapFrom(src => src.Mtn_Desc))
                     .ForMember(dest => dest.Rcv_Date, opt => opt.MapFrom(src => src.RcvDate))
                     .ForMember(dest => dest.Coffee_Cup, opt => opt.MapFrom(src => src.CoffeeCup))
                     .ForMember(dest => dest.AppClose_Date, opt => opt.MapFrom(src => src.AppCloseDate))
                     .ForMember(dest => dest.VndEng_Id, opt => opt.MapFrom(src => src.VndEngId))
                     .ForMember(dest => dest.Pre_Amt, opt => opt.MapFrom(src => src.Pre_Amt))
                     .IgnoreAllNonExisting();
            }
        }


        #endregion


        #region 案件歷程
        private class TCallogCourseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TCallogCourse, TCallogCourse>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.Assignor, opt => opt.MapFrom(src => src.Assignor))
                     .ForMember(dest => dest.Admissibility, opt => opt.MapFrom(src => src.Admissibility))
                     .ForMember(dest => dest.Datetime, opt => opt.MapFrom(src => src.Datetime))
                     .IgnoreAllNonExisting();
            }
        }

        private class TCallogCourseReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TCallogCourse, DataBase.TCallogCourse>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.Assignor, opt => opt.MapFrom(src => src.Assignor))
                     .ForMember(dest => dest.Admissibility, opt => opt.MapFrom(src => src.Admissibility))
                     .ForMember(dest => dest.Datetime, opt => opt.MapFrom(src => src.Datetime))
                     .IgnoreAllNonExisting();
            }
        }


        #endregion

        #region seteng使用者


        private class TusrdtlProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TUSRDTL, Tusrdtl>()
                    .ForMember(dest => dest.ChgPasSts, opt => opt.MapFrom(src => src.ChgPas_Sts))
                    .ForMember(dest => dest.LoginDateTime, opt => opt.MapFrom(src => src.Login_DateTime))
                    .ForMember(dest => dest.LoginErrTimes, opt => opt.MapFrom(src => src.Login_Err_Times))
                    .ForMember(dest => dest.LoginTimes, opt => opt.MapFrom(src => src.Login_Times))
                    .ForMember(dest => dest.PassWd, opt => opt.MapFrom(src => src.Pass_Wd))
                    .ForMember(dest => dest.PwdChgTime, opt => opt.MapFrom(src => src.Pwd_Chg_Time))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_Id))
                    .ForMember(dest => dest.TUSRMST, opt => opt.MapFrom(src => src.TUSRMST))
                    .IgnoreAllNonExisting();
            }
        }

        private class TusrdtlReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tusrdtl, DataBase.TUSRDTL>()
                    .ForMember(dest => dest.ChgPas_Sts, opt => opt.MapFrom(src => src.ChgPasSts))
                    .ForMember(dest => dest.Login_DateTime, opt => opt.MapFrom(src => src.LoginDateTime))
                    .ForMember(dest => dest.Login_Err_Times, opt => opt.MapFrom(src => src.LoginErrTimes))
                    .ForMember(dest => dest.Login_Times, opt => opt.MapFrom(src => src.LoginTimes))
                    .ForMember(dest => dest.Pass_Wd, opt => opt.MapFrom(src => src.PassWd))
                    .ForMember(dest => dest.Pwd_Chg_Time, opt => opt.MapFrom(src => src.PwdChgTime))
                    .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.TUSRMST, opt => opt.MapFrom(src => src.TUSRMST))
                    .IgnoreAllNonExisting();
            }
        }

        private class TusrmstProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TUSRMST, Tusrmst>()

                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.EmailAccount, opt => opt.MapFrom(src => src.Email_Account))
                    .ForMember(dest => dest.IdSts, opt => opt.MapFrom(src => src.Id_Sts.Equals("Y")))
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role_Id))
                    .ForMember(dest => dest.TelNo, opt => opt.MapFrom(src => src.Tel_No))
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_Date))
                    .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User_Id))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User_Name))
                    .ForMember(dest => dest.PageAuth, opt => opt.MapFrom(src => src.PageAuth))
                    .ForMember(dest => dest.TUSRDTL, opt => opt.MapFrom(src => src.TUSRDTL))
                    .IgnoreAllNonExisting();
            }
        }

        private class TusrmstReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tusrmst, DataBase.TUSRMST>()

                    .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                    .ForMember(dest => dest.Email_Account, opt => opt.MapFrom(src => src.EmailAccount))
                    .ForMember(dest => dest.Id_Sts, opt => opt.MapFrom(src => src.IdSts ? "Y" : "N"))
                    .ForMember(dest => dest.Role_Id, opt => opt.MapFrom(src => src.RoleId))
                    .ForMember(dest => dest.Tel_No, opt => opt.MapFrom(src => src.TelNo))
                    .ForMember(dest => dest.Update_Date, opt => opt.MapFrom(src => src.UpdateDate))
                    .ForMember(dest => dest.Update_User, opt => opt.MapFrom(src => src.UpdateUser))
                    .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.User_Name, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.Vender_Id, opt => opt.MapFrom(src => src.VenderCd))
                    .ForMember(dest => dest.PageAuth, opt => opt.MapFrom(src => src.PageAuth))
                    .ForMember(dest => dest.TUSRDTL, opt => opt.MapFrom(src => src.TUSRDTL))
                    .IgnoreAllNonExisting();
            }
        }


        #endregion

        #region 技師群組主檔

        private class TtechnicianGroupProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TTechnicianGroup, TtechnicianGroup>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.VendorCd, opt => opt.MapFrom(src => src.VendorCd))
                     .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
                     .ForMember(dest => dest.Responsible_Zo, opt => opt.MapFrom(src => src.Responsible_Zo))
                     .ForMember(dest => dest.Responsible_Do, opt => opt.MapFrom(src => src.Responsible_Do))
                     .ForMember(dest => dest.Seq, opt => opt.MapFrom(src => src.Seq))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }
        private class TtechnicianGroupReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TtechnicianGroup, DataBase.TTechnicianGroup>()
                     .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.VendorCd, opt => opt.MapFrom(src => src.VendorCd))
                     .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupName))
                     .ForMember(dest => dest.Responsible_Zo, opt => opt.MapFrom(src => src.Responsible_Zo))
                     .ForMember(dest => dest.Responsible_Do, opt => opt.MapFrom(src => src.Responsible_Do))
                     .ForMember(dest => dest.Seq, opt => opt.MapFrom(src => src.Seq))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }


        private class TTechnicianGroupClaimsProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims>()
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }

        #endregion

        #region 案件受理主檔
        private class TacceptLogProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TAcceptedLog, TacceptedLog>()
                     .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                     .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                     .ForMember(dest => dest.RcvDatetime, opt => opt.MapFrom(src => src.RcvDatetime))
                     .ForMember(dest => dest.RcvRemark, opt => opt.MapFrom(src => src.RcvRemark))
                     .ForMember(dest => dest.Sn, opt => opt.MapFrom(src => src.Sn))
                     .ForMember(dest => dest.TCALLOG, opt => opt.MapFrom(src => src.TCALLOG))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }
        #endregion

        #region 推播記錄檔

        private class TcallogRecordProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TCallLogRecord, TcallogRecord>()
                     .ForMember(dest => dest.RecordDatetime, opt => opt.MapFrom(src => src.RecordDatetime))
                     .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                     .ForMember(dest => dest.RecordRemark, opt => opt.MapFrom(src => src.RecordRemark))
                     .ForMember(dest => dest.SN, opt => opt.MapFrom(src => src.SN))
                     .ForMember(dest => dest.TCALLOG, opt => opt.MapFrom(src => src.TCALLOG))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }
        private class TcallogRecordReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TcallogRecord, DataBase.TCallLogRecord>()
                     .ForMember(dest => dest.RecordDatetime, opt => opt.MapFrom(src => src.RecordDatetime))
                     .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                     .ForMember(dest => dest.RecordRemark, opt => opt.MapFrom(src => src.RecordRemark))
                     .ForMember(dest => dest.SN, opt => opt.MapFrom(src => src.SN))
                     .ForMember(dest => dest.TCALLOG, opt => opt.MapFrom(src => src.TCALLOG))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }

        #endregion

        #region 廠商服務公司檔
        private class TUSRVENRELATIONProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TUSRVENRELATION, TUSRVENRELATION>()
                    .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.User_Id))
                    .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.Vender_Cd))
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        private class TUSRVENRELATIONReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<TUSRVENRELATION, DataBase.TUSRVENRELATION>()
                     .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.User_Id))
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.Vender_Cd))
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }

        #endregion

        #region 廠商區域檔
        private class TVNDZOProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TVNDZO, Tvndzo>()
                    .ForMember(dest => dest.VenderCd, opt => opt.MapFrom(src => src.Vender_Cd))
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.Zo, opt => opt.MapFrom(src => src.Z_O))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        private class TVNDZOReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tvndzo, DataBase.TVNDZO>()
                     .ForMember(dest => dest.Vender_Cd, opt => opt.MapFrom(src => src.VenderCd))
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Z_O, opt => opt.MapFrom(src => src.Zo))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }

        #endregion

        #region 區域主檔
        private class TZOCODEProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TZOCODE, Tzocode>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.ZoCd, opt => opt.MapFrom(src => src.Z_O))
                    .ForMember(dest => dest.ZoName, opt => opt.MapFrom(src => src.Zo_Name))
                    .ForMember(dest => dest.DoCd, opt => opt.MapFrom(src => src.D_O))
                    .ForMember(dest => dest.DoName, opt => opt.MapFrom(src => src.Do_Name))
                    .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.Close_Date))
                    .ForMember(dest => dest.ZoType, opt => opt.MapFrom(src => src.Zo_Type))
                    .ForMember(dest => dest.ZoAstCd, opt => opt.MapFrom(src => src.Zo_Ast_Cd))
                    .ForMember(dest => dest.UpkeepSts, opt => opt.MapFrom(src => src.Upkeep_Sts))
                    .ForMember(dest => dest.CallSts, opt => opt.MapFrom(src => src.Call_Sts))
                    .ForMember(dest => dest.PrjSts, opt => opt.MapFrom(src => src.Prj_Sts))
                    .ForMember(dest => dest.ActSts, opt => opt.MapFrom(src => src.Act_Sts))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        private class TZOCODEReverseProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Tzocode, DataBase.TZOCODE>()
                     .ForMember(dest => dest.Comp_Cd, opt => opt.MapFrom(src => src.CompCd))
                     .ForMember(dest => dest.Z_O, opt => opt.MapFrom(src => src.ZoCd))
                     .ForMember(dest => dest.Zo_Name, opt => opt.MapFrom(src => src.ZoName))
                     .ForMember(dest => dest.D_O, opt => opt.MapFrom(src => src.DoCd))
                     .ForMember(dest => dest.Do_Name, opt => opt.MapFrom(src => src.DoName))
                     .ForMember(dest => dest.Close_Date, opt => opt.MapFrom(src => src.CloseDate))
                     .ForMember(dest => dest.Zo_Type, opt => opt.MapFrom(src => src.ZoType))
                     .ForMember(dest => dest.Zo_Ast_Cd, opt => opt.MapFrom(src => src.ZoAstCd))
                     .ForMember(dest => dest.Upkeep_Sts, opt => opt.MapFrom(src => src.UpkeepSts))
                     .ForMember(dest => dest.Call_Sts, opt => opt.MapFrom(src => src.CallSts))
                     .ForMember(dest => dest.Prj_Sts, opt => opt.MapFrom(src => src.PrjSts))
                     .ForMember(dest => dest.Act_Sts, opt => opt.MapFrom(src => src.ActSts))
                     .IgnoreAllNonExisting().ReverseMap();
            }
        }

        #endregion

        #region 設備主檔
        private class TASSETSProfile : Profile
        {
            protected override void Configure()
            { //, , , , , , , Debit_Kind, First_Ast_Cd, Maintain_Charges, Repair_Charges, Staging_Charges, Sum_Usual_Limit, Win_Usual_Limit, Sum_Emc_Limit, Win_Emc_Limit, Provider_No, Costdown_B, Costdown_C, Costdown_D, Upkeep_Cal_Type, Ast_Limit, Close_Date, Sap_Project_No, Spc_Asset_Kind, Update_User, Update_Date, Staging_Month
                CreateMap<DataBase.TASSETS, Tassets>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.AssetCd, opt => opt.MapFrom(src => src.Asset_Cd))
                    .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset_Name))
                    .ForMember(dest => dest.AstKind1, opt => opt.MapFrom(src => src.Ast_Kind1))
                    .ForMember(dest => dest.AstKind2, opt => opt.MapFrom(src => src.Ast_Kind2))
                    .ForMember(dest => dest.AstKind3, opt => opt.MapFrom(src => src.Ast_Kind3))
                    .ForMember(dest => dest.Debit_Kind, opt => opt.MapFrom(src => src.Debit_Kind))
                    .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_Date))
                    .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.Close_Date))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        #endregion

        #region 設備故障處理代號檔

        private class TDAMMTNProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TDAMMTN, Tdammtn>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.AssetCd, opt => opt.MapFrom(src => src.Asset_Cd))
                    .ForMember(dest => dest.DamageProcNo, opt => opt.MapFrom(src => src.Damage_Proc_No))
                    .ForMember(dest => dest.MtnDesc, opt => opt.MapFrom(src => src.Mtn_Desc))
                    .ForMember(dest => dest.CountSts, opt => opt.MapFrom(src => src.Count_Sts))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        #endregion

        #region 銷案類型檔

        private class TFINISHProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TFINISH, Tfinish>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.FinishId, opt => opt.MapFrom(src => src.Finish_Id))
                    .ForMember(dest => dest.FinishName, opt => opt.MapFrom(src => src.Finish_Name))
                    .ForMember(dest => dest.CountSts, opt => opt.MapFrom(src => src.Count_Sts))
                    .ForMember(dest => dest.VenderSts, opt => opt.MapFrom(src => src.Vender_Sts))
                    .ForMember(dest => dest.FinishSts, opt => opt.MapFrom(src => src.Finish_Sts))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        #endregion

        #region 工作類別檔

        private class TWRKKNDProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TWRKKND, Twrkknd>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.WorkId, opt => opt.MapFrom(src => src.Work_Id))
                    .ForMember(dest => dest.WorkDesc, opt => opt.MapFrom(src => src.Work_Desc))
                    .ForMember(dest => dest.Worksts, opt => opt.MapFrom(src => src.Comp_Cd))
                    .IgnoreAllNonExisting().ReverseMap();
            }
        }
        #endregion

        private class TrefdatProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TREFDAT, Trefdat>()
                    .ForMember(dest => dest.LabelCd, opt => opt.MapFrom(src => src.Label_Cd))
                    .ForMember(dest => dest.KeyCd, opt => opt.MapFrom(src => src.Key_Cd))
                    .ForMember(dest => dest.RefData, opt => opt.MapFrom(src => src.Ref_Data))
                    .ForMember(dest => dest.RefNote, opt => opt.MapFrom(src => src.Ref_Note))
                    .IgnoreAllNonExisting()
                    .ReverseMap();
            }
        }


        private class TstsdatProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TSYSDAT, Tsysdat>()
                    .ForMember(dest => dest.KeyCd, opt => opt.MapFrom(src => src.Key_Cd))
                    .ForMember(dest => dest.SmptName, opt => opt.MapFrom(src => src.Smpt_Name))
                    .ForMember(dest => dest.SendMail, opt => opt.MapFrom(src => src.Send_Mail))
                    .ForMember(dest => dest.MailPassword, opt => opt.MapFrom(src => src.Mail_Password))
                    .IgnoreAllNonExisting()
                    .ReverseMap();
            }
        }

        private class TstrmstProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<DataBase.TSTRMST, Tstrmst>()
                    .ForMember(dest => dest.CompCd, opt => opt.MapFrom(src => src.Comp_Cd))
                    .ForMember(dest => dest.StoreCd, opt => opt.MapFrom(src => src.Store_Cd))
                    .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store_Name))
                    .ForMember(dest => dest.StoreKind, opt => opt.MapFrom(src => src.Store_Kind))
                    .ForMember(dest => dest.OpenDate, opt => opt.MapFrom(src => src.Open_Date))
                    .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.Close_Date))
                    .ForMember(dest => dest.ZoCd, opt => opt.MapFrom(src => src.Z_O))
                    .ForMember(dest => dest.ZoName, opt => opt.MapFrom(src => src.Zo_Name))
                    .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.Update_User))
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Update_Date))
                    .IgnoreAllNonExisting()
                    .ReverseMap();
            }
        }

    }
}