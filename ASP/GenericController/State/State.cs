using GenericController.Entity;
using Microsoft.AspNetCore.Mvc;


namespace GenericController.State
{
    public static class State
    {
       public static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }
        public static ResponseData GetResponse(this ControllerBase cBase, object response= null, int status=200, object err= null)
        {
            cBase.HttpContext.Response.StatusCode = status;
            return new ResponseData();
        }

    }
}

