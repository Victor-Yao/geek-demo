using System;
using Microsoft.Extensions.Configuration;
using CustomProviderDemo;

namespace Microsoft.Extensions.Configuration
{
    public static class MyConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new MyConfigurationSource());
            return builder;
        }
    }
}