using AspNetIdentity.WebApi.Controllers;
using AspNetIdentity.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.WebApi.Services
{
    public class StatusUtility
    {
        public void updateMemberInfo(int id, int memberId, string opt)
        {
            MemberUtility utilObj = new MemberUtility();
            Member data = utilObj.getMemberInfo(memberId);
            if(opt == "SalStatus")
            {
                data.AssuranceId = id;
            }
            else if (opt== "BapStatus")
            {
                data.BaptismStatusId = id;
            }
            MembersController memberController = new MembersController();
            memberController.PutMember(data.MemberId, data);
        }
    }
}