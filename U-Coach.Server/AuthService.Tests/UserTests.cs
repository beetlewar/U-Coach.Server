﻿using DevOne.Security.Cryptography.BCrypt;
using NUnit.Framework;
using PVDevelop.UCoach.Server;
using PVDevelop.UCoach.Server.AuthService;
using PVDevelop.UCoach.Server.Exceptions.Auth;
using PVDevelop.UCoach.Server.Mongo;
using Rhino.Mocks;
using System;
using Timing;
using TestUtils;

namespace AuthService.Tests
{
    [TestFixture]
    public class UserTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Ctor_EmptyLogin_ThrowsException(string login)
        {
            Assert.Throws(typeof(LoginNotSetException), () => new User(MockRepository.GenerateStub<IUtcTimeProvider>(), login));
        }

        [Test]
        public void Ctor_ValidLogin_SetsCreationTime()
        {
            var timeProvider = new FixedUtcTimeProvider();
            var user = new User(timeProvider, "u");
            Assert.AreEqual(timeProvider.UtcTime, user.CreationTime);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void SetPassword_InvalidPasswordFormat_ThrowsException(string password)
        {
            var user = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "a");
            Assert.Throws(typeof(InvalidPasswordFormatException), () => user.SetPassword(password));
        }

        [Test]
        public void Logon_InvalidPassword_ThrowsException()
        {
            var user = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "abc");
            user.SetPassword("pwd");
            Assert.Throws(typeof(InvalidPasswordException), () => user.Logon("invalid_password"));
        }

        [Test]
        public void Logon_ValidPassword_SetsLoggedIn()
        {
            var user = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "abc");
            user.SetPassword("pwd");
            user.Logon("pwd");
            Assert.IsTrue(user.IsLoggedIn);
        }

        [Test]
        public void Logon_ValidPassword_ReturnsExpectedToken()
        {
            var user = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "u");

            user.SetPassword("pwd123");
            var token = user.Logon("pwd123");

            Assert.IsTrue(BCryptHelper.CheckPassword(user.Password, token));
        }

        [Test]
        public void Logon_ValidPassword_SetsCurrentAuthenticationTime()
        {
            var timeProvider = new FixedUtcTimeProvider();
            var user = new User(timeProvider, "abc");
            user.SetPassword("pwd3");
            user.Logon("pwd3");
            Assert.AreEqual(timeProvider.UtcTime, user.LastAuthenticationTime);
        }

        [Test]
        public void Logout_UserIsLoggedIn_SetsNotLoggedIn()
        {
            var user = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "aaa");
            user.SetPassword("pwd");
            user.Logon("pwd");
            user.Logout("pwd");
            Assert.IsFalse(user.IsLoggedIn);
        }

        [Test]
        public void Logout_UserIsLoggedInButInvalidPassword_ThrowsException()
        {
            var user = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "aaa");
            user.SetPassword("pwd");
            user.Logon("pwd");
            Assert.Throws(typeof(InvalidPasswordException), () => user.Logout("invalid"));
        }

        [Test]
        public void ValidateToken_ValidToken_DoesNothing()
        {
            var u = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "u");
            u.SetPassword("p1");
            var token = u.Logon("p1");
            u.ValidateToken(token);
        }

        [Test]
        public void ValidateToken_InvalidToken_ThrowsException()
        {
            var u = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "u");
            u.SetPassword("p1");
            u.Logon("p1");
            Assert.Throws(typeof(InvalidTokenException), () => u.ValidateToken("abc"));
        }

        [Test]
        public void ValidateToken_UserIsNotLoggedIn_ThrowsException()
        {
            var u = new User(MockRepository.GenerateStub<IUtcTimeProvider>(), "aaa");
            u.SetPassword("p2");
            var token = u.Logon("p2");
            u.Logout("p2");
            Assert.Throws(typeof(NotLoggedInException), () => u.ValidateToken(token));
        }
    }
}