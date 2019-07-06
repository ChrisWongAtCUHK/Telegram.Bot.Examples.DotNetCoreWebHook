# .NET Core Web Hook Example Heroku Deployment

## About
[.NET Core Web Hook Example](https://github.com/TelegramBots/Telegram.Bot.Examples/tree/master/Telegram.Bot.Examples.DotNetCoreWebHook)

## Setup
This is an short description how you can deploy your bot on Heroku. The description presumes that you already:
1. have a bot and it’s token. If not, please create one. You’ll find several explanations on the internet how to do this.
2. have a Heroku account

### Create Heroku app
```
heroku apps:create ${DOTNETCORE_APP_NAME} --buildpack jincod/dotnetcore

heroku config:set BOT_ACCESS_TOKEN=${BOT_ACCESS_TOKEN}
```

### Set Webhook
```
curl -X POST https://api.telegram.org/bot${BOT_ACCESS_TOKEN}/setWebhook?url=${DOTNETCORE_APP_NAME}.herokuapp.com/api/update
```

### Deploy
```
git push heroku master
```

### Open
```
heroku open
```