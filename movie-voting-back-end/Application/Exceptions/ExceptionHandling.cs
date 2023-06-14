namespace WebAPI.Application.Exceptions
{
    using System;
    //Creating our own Exception Class by inheriting Exception class
    public class ExceptionHandling : ApplicationException
    {
        public ExceptionHandling()
        {
        }

        public ExceptionHandling(string message)
            : base(message)
        {
        }

        public ExceptionHandling(string message, Exception inner)
            : base(message, inner)
        {
        }

        //Overriding the HelpLink Property
        public override string HelpLink
        {
            get
            {
                return "Get More Information from here: https://dotnettutorials.net/lesson/create-custom-exception-csharp/";
            }
        }
    }
}
