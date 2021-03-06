﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Karambolo.AspNetCore.Bundling.Internal
{
    public class BundleFileProvider : IFileProvider
    {
        readonly IBundleManager _bundleManager;
        readonly IHttpContextAccessor _httpContextAccessor;

        public BundleFileProvider(IBundleManager bundleManager, IHttpContextAccessor httpContextAccessor)
        {
            _bundleManager = bundleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return NotFoundDirectoryContents.Singleton;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext != null ? _bundleManager.GetFileInfo(httpContext) : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }
    }
}
