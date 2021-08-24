using System;
using Xunit;
using UrlShortener.Data;
using FluentAssertions;
using System.Collections.Generic;

namespace UrlShortener.Tests
{
    public class UrlValidatorTests
    {
        [Fact]
        public void IsValidUrl_Test()
        {
            var validator = new UrlValidator();
            var validUrls = new List<string> { "http://www.google.com", "www.google.com", "google.com" };
            var invalidUrls = new List<string> { "http://www.google.comhttp://www.google.com", "www.go...ogle.com", "go''gle.com" };

            foreach (var url in validUrls)
            {
                validator.IsValidUrl(url).Should().BeTrue();
                validator.IsValidUrl(url).Should().BeFalse();
            }
        }
    }
}

