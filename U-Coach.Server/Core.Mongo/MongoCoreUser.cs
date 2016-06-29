﻿using PVDevelop.UCoach.Server.Core.Domain;
using PVDevelop.UCoach.Server.Mongo;
using System;

namespace PVDevelop.UCoach.Server.Core.Mongo
{
    [MongoCollection("CoreUsers")]
    [MongoDataVersion(VERSION)]
    public class MongoCoreUser
    {
        /// <summary>
        /// Текущая версия документа.
        /// </summary>
        private const int VERSION = 1;

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Версия документа.
        /// </summary>
        public int Version { get; set; }

        [MongoIndexName("auth_system")]
        /// <summary>
        /// Система аутентификации.
        /// </summary>
        public CoreUserAuthSystem AuthSystem { get; set; }

        [MongoIndexName("auth_id")]
        /// <summary>
        /// Идентификатор пользователя в системе аутентификации.
        /// </summary>
        public string AuthId { get; set; }

        /// <summary>
        /// Ключ подтверждения.
        /// </summary>
        public string ConfirmationKey { get; set; }

        /// <summary>
        /// Состояние пользователя.
        /// </summary>
        public CoreUserState State { get; set; }

        public MongoCoreUser()
        {
            Version = VERSION;
        }
    }
}