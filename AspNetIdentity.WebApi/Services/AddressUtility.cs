using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AspNetIdentity.WebApi.Controllers;
using AspNetIdentity.WebApi.Models;

namespace AspNetIdentity.WebApi.Services
{
    public class AddressUtility
    {
        public int addAddress(Address address)
        {
            AddressesController serviceObject = new AddressesController();
            int addressId = serviceObject.PostAddress(address);
            return addressId;
        }

        public bool updateAddress(Address address)
        {
            AddressesController serviceObject = new AddressesController();
            var addressData = serviceObject.PutAddress(address.AddressId, address);
            if(addressData != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}