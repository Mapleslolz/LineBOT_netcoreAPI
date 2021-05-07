using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace isRock.Template
{
    public class LineWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        [Route("api/LineBotWebHook")]
        [HttpPost]
        public IActionResult POST()
        {
            var AdminUserId = "_______U5e60294b8c__AdminUserId__02d6295b621a_____";

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "_____________ChannelAccessToken___________________";
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //取得UserInfo
                var user = this.GetUserInfo(LineEvent.source.userId);
                //配合Line verify 
                if (LineEvent == null) return Ok();
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                var responseMsg = "";
                var ImgMap = new Uri("https://i.imgur.com/");
                //準備回覆訊息
                Console.WriteLine(user.displayName + "說：" + LineEvent.message.text);
                if (LineEvent.message.text == "太爽鳥" && LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                { // send image
                    ImgMap = new Uri("https://i.imgur.com/carBtrk.png");
                }
                else if (LineEvent.message.text == "哭阿" || LineEvent.message.text == "哭啊" && LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                { // send image
                    ImgMap = new Uri("https://i.imgur.com/a4q0Jad.jpg");
                }
                else if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                    responseMsg = $"你說了: {LineEvent.message.text}";
                // responseMsg = $"{user.displayName}說了：{LineEvent.message.text}";
                else if (LineEvent.type.ToLower() == "message")
                    responseMsg = $"收到 event : {LineEvent.type} type: {LineEvent.message.type} ";
                else
                    responseMsg = $"收到 event : {LineEvent.type} ";
                //回覆訊息
                if (responseMsg != "")
                    this.ReplyMessage(LineEvent.replyToken, responseMsg);
                else
                    this.ReplyMessage(LineEvent.replyToken, ImgMap);

                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}