using System;
using System.Diagnostics;

namespace Bat.Core
{
    public class ExceptionBusiness
    {
        /// <summary>
        /// Get Caller Method Name
        /// </summary>
        /// <param name="error">Exception</param>
        /// <returns>ExceptionDetails</returns>
        public static ExceptionDetails GetCallerMethodName(Exception error)
        {
            var result = new ExceptionDetails();
            var parameters = string.Empty;
            var callerMethod = new StackFrame(2).GetMethod();
            if (callerMethod.IsGenericMethod)
            {
                #region Generic Method
                foreach (var param in callerMethod.GetParameters())
                    parameters += param.Name + ":" + param.ParameterType.Name + " | ";
                var callerMethodName = callerMethod.DeclaringType != null ? callerMethod.DeclaringType.FullName : string.Empty;
                callerMethodName += "." + callerMethod.Name + "()";
                var errorFrame = new StackTrace(error, true).GetFrame(0);

                result.MethodName = callerMethodName;
                result.Parameters = parameters;
                result.ExceptionLineNumber = errorFrame == null ? -1 : errorFrame.GetFileLineNumber();
                return result;
                #endregion
            }
            else
            {
                #region General
                foreach (var param in callerMethod.GetParameters())
                    parameters += param.Name + ":" + param.ParameterType.Name + " | ";
                var callerMethodName = callerMethod.DeclaringType != null ? callerMethod.DeclaringType.FullName : string.Empty;
                callerMethodName += "." + callerMethod.Name + "()";
                var errorFrame = new StackTrace(error, true).GetFrame(0);

                result.MethodName = callerMethodName;
                result.Parameters = parameters;
                result.ExceptionLineNumber = errorFrame == null ? -1 : errorFrame.GetFileLineNumber();
                return result;
                #endregion
            }
        }

        /// <summary>
        /// Get Caller Method Name In AOP Mode
        /// </summary>
        /// <param name="error">Exception</param>
        /// <returns>ExceptionDetails</returns>
        public static ExceptionDetails AOPGetCallerMethodName(Exception error)
        {
            var result = new ExceptionDetails();
            var parameters = string.Empty;
            var callerMethod = new StackFrame(3).GetMethod();

            foreach (var param in callerMethod.GetParameters())
                parameters += param.Name + ":" + param.ParameterType.Name + " | ";
            var callerMethodName = callerMethod.DeclaringType != null ? callerMethod.DeclaringType.FullName : string.Empty;
            callerMethodName += "." + callerMethod.Name + "()";
            var errorFrame = new StackTrace(error, true).GetFrame(0);

            result.MethodName = callerMethodName;
            result.Parameters = parameters;
            result.ExceptionLineNumber = errorFrame == null ? -1 : errorFrame.GetFileLineNumber();
            return result;
        }
    }
}
