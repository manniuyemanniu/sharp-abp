// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using SharpAbp.Abp.Identity;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.Identity.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IIdentitySettingsAppService), typeof(IdentitySettingsClientProxy))]
public partial class IdentitySettingsClientProxy : ClientProxyBase<IIdentitySettingsAppService>, IIdentitySettingsAppService
{
    public virtual async Task<IdentitySettingsDto> GetAsync()
    {
        return await RequestAsync<IdentitySettingsDto>(nameof(GetAsync));
    }

    public virtual async Task UpdateAsync(UpdateIdentitySettingsDto input)
    {
        await RequestAsync(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UpdateIdentitySettingsDto), input }
        });
    }
}
