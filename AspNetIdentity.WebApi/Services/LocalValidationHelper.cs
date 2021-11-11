using AspNetIdentity.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetIdentity.WebApi.Controllers;

namespace AspNetIdentity.WebApi.Services
{
    public class LocalValidationHelper
    {
        public bool checkUserValidity(string userName, int memberId)
        {
            MemberUser bridgeTable = new MemberUsersController().GetMemberUserLocal(userName);
            Member memberClient = new MemberUtility().getMemberInfo(int.Parse(bridgeTable.MemberId));
            Member memberTarget = new MemberUtility().getMemberInfo(memberId);
            if(checkAuthorization(memberClient.LocalAssemblyId, memberTarget.LocalAssemblyId))
            {
                return true;
            }
            return false;
        }

        public bool checkWriteAccess(string userName, int memberId)
        {
            MemberUser bridgeTable = new MemberUsersController().GetMemberUserLocal(userName);
            Member memberClient = new MemberUtility().getMemberInfo(int.Parse(bridgeTable.MemberId));
            Member memberTarget = new MemberUtility().getMemberInfo(memberId);
            if(memberClient.LocalAssemblyId == memberClient.LocalAssemblyId)
            {
                return true;
            }
            return false;
        }

        public bool checkAuthorization(int client, int target)
        {
            MemberUtility utilObj = new MemberUtility();
            if(client == target)
            {
                return true;
            }
            else if(utilObj.getUserMetaId(client, "district") == utilObj.getUserMetaId(target, "district")
                || utilObj.getUserMetaId(client, "area") == utilObj.getUserMetaId(target, "area")
                || utilObj.getUserMetaId(client, "rcc") == utilObj.getUserMetaId(target, "rcc")
                || utilObj.getUserMetaId(client, "nat") == utilObj.getUserMetaId(target, "nat")
                || utilObj.getUserMetaId(client, "block") == utilObj.getUserMetaId(target, "block"))
            {
                return true;
            }
            return false;
        }
    }
}