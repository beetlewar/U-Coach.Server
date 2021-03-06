﻿using PVDevelop.UCoach.Server.Core.Domain.Exceptions;
using PVDevelop.UCoach.Server.Domain;
using System;

namespace PVDevelop.UCoach.Server.Core.Domain
{
    public class SportsmanConfirmation : 
        AAggregateRoot
    {
        public string AuthUserId { get; internal set; }

        public SportsmanConfirmationAuthSystem AuthSystem { get; internal set; }

        public string ConfirmationKey { get; internal set; }

        public SportsmanConfirmationState State { get; private set; }

        internal SportsmanConfirmation() { }

        public void Confirm(string confirmationKey)
        {
            if(confirmationKey != ConfirmationKey)
            {
                throw new InvalidConfirmationKeyException();
            }

            State = SportsmanConfirmationState.Confirmed;
        }
    }
}
