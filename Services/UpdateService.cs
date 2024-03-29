﻿using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Examples.DotNetCoreWebHook.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;
        private readonly ILogger<UpdateService> _logger;

        public UpdateService(IBotService botService, ILogger<UpdateService> logger)
        {
            _botService = botService;
            _logger = logger;
        }

        public async Task EchoAsync(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var message = update.Message;
                    if (message == null || message.Type != MessageType.Text) return;

                    _logger.LogInformation("Received Message from {0}", message.Chat.Id);

                    switch (message.Text.Split(' ').First())
                    {
                        // send inline keyboard
                        case "/inline":
                            await _botService.Client.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                            await Task.Delay(500); // simulate longer running task

                            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                            {
                                new [] // first row
                                {
                                    InlineKeyboardButton.WithCallbackData("1.1"),
                                    InlineKeyboardButton.WithCallbackData("1.2"),
                                },
                                new [] // second row
                                {
                                    InlineKeyboardButton.WithCallbackData("2.1"),
                                    InlineKeyboardButton.WithCallbackData("2.2"),
                                }
                            });

                            await _botService.Client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Choose",
                                replyMarkup: inlineKeyboard);
                            break;

                        // send custom keyboard
                        case "/keyboard":
                            ReplyKeyboardMarkup ReplyKeyboard = new[]
                            {
                                new[] { "1.1", "1.2" },
                                new[] { "2.1", "2.2" },
                            };

                            await _botService.Client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Choose",
                                replyMarkup: ReplyKeyboard);
                            break;

                        // send a photo
                        case "/photo":
                            try
                            {
                                await _botService.Client.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

                                var req = HttpWebRequest.Create("https://i.imgur.com/l8WqVDx.png");

                                using(var stream = req.GetResponse().GetResponseStream())
                                {
                                    await _botService.Client.SendPhotoAsync(
                                        message.Chat.Id,
                                        stream,
                                        "Nice Picture");
                                }
                            }
                            catch (System.Exception e)
                            {
                                _logger.LogInformation("Exception:{0}", e.Message);
                                throw e;
                            }
                            break;

                        // request location or contact
                        case "/request":
                            var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                            {
                                KeyboardButton.WithRequestLocation("Location"),
                                KeyboardButton.WithRequestContact("Contact"),
                            });

                            await _botService.Client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Who or Where are you?",
                                replyMarkup: RequestReplyKeyboard);
                            break;

                        default:
                            const string usage = @"
Usage:
/inline   - send inline keyboard
/keyboard - send custom keyboard
/photo    - send a photo
/request  - request location or contact";

                            await _botService.Client.SendTextMessageAsync(
                                message.Chat.Id,
                                usage,
                                replyMarkup: new ReplyKeyboardRemove());
                            break;
                    }
                    break;
                case UpdateType.CallbackQuery:
                    var callbackQuery = update.CallbackQuery;
                    await _botService.Client.AnswerCallbackQueryAsync(
                    callbackQuery.Id,
                    $"Received {callbackQuery.Data}");

                    await _botService.Client.SendTextMessageAsync(
                    callbackQuery.Message.Chat.Id,
                    $"Received {callbackQuery.Data}");
                    break;
                default:
                    break;
            }
        }
    }
}
