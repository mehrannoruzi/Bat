using System.Collections.Generic;

namespace Bat.Core
{
    public interface IEmailService
    {
        IResponse<List<bool>> Send(string from, List<string> to, EmailMessage email);
        IResponse<List<bool>> Send(List<string> from, List<string> to, EmailMessage email);
        IResponse<List<bool>> Send(List<string> from, List<string> to, List<EmailMessage> email);
    }
}
