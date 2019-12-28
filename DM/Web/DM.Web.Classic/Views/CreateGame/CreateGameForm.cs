﻿using System;
using System.ComponentModel.DataAnnotations;
using DM.Services.Core.Dto.Enums;

namespace DM.Web.Classic.Views.CreateGame
{
    public class CreateGameForm
    {
        [Required(ErrorMessage = "Введите название игры")]
        [StringLength(100, ErrorMessage = "не больше {1} символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите название ролевой системы")]
        [StringLength(50, ErrorMessage = "не больше {1} символов")]
        public string SystemName { get; set; }

        [Required(ErrorMessage = "Введите название игрового сеттинга")]
        [StringLength(50, ErrorMessage = "не больше {1} символов")]
        public string SettingName { get; set; }

        [Required(ErrorMessage = "Введите информацию об игре")]
        [StringLength(int.MaxValue, MinimumLength = 200,
            ErrorMessage = "Информация о модуле должна содержать больше {2} символов")]
        public string Info { get; set; }

        public bool CreateAsRegistration { get; set; }

        public Guid AttributeSchemaId { get; set; }

        public Guid? AssistantId { get; set; }

        public bool HideTemper { get; set; }
        public bool HideSkills { get; set; }
        public bool HideInventory { get; set; }
        public bool HideStory { get; set; }
        public bool HideDiceResults { get; set; }
        public bool DisableAlignment { get; set; }
        public bool ShowPrivateMessages { get; set; }

        public CommentariesAccessMode CommentariesAccessMode { get; set; }

        public Guid[] TagIds { get; set; }
        public Guid? SchemaId => AttributeSchemaId == NoSchema ? (Guid?) null : AttributeSchemaId;

        public static Guid NoSchema = Guid.Parse("cc2387b2-066f-4178-9dde-4daafeea91c0");
    }
}