using DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace IAppServices
{
    public interface IUserAppService: IApplicationService,IValidateAppService
    {
        ValidatorOutput ValidatorUserInput(UserInputValidator input);
    }
}
