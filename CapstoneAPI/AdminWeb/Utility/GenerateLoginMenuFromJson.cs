using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wisky.Data.Utility;
using Wisky.Models;

namespace Wisky.Ultility
{
    public class GenerateLoginMenuFromJson
    {
        public static LoginMenuModel GenerateLoginMenuModel(int TypeOfMenu)
        {
            //LoginMenuModel loginMenu = JsonConvert.DeserializeObject<LoginMenuModel>(controlJson);
            if (TypeOfMenu == (int)LoginMenuModel.MenuType.Vertical)
            {
                LoginMenuModel loginMenu = new LoginMenuModel()
                {
                    Id = "general-form",
                    InlineCss =
                        "font-family: roboto; position: relative; width:35%; top:0; left:0; padding: 10px;text-align: center;color: #fff;border-radius: 0 0 4px 4px;background-color: #000;background-color: rgba(0,0,0,.5);animation: playMargin 1.5s;opacity: 100;",
                    X = 32.5,
                    Y = 7.9,
                    GroupItemsType = (int)LoginMenuModel.MenuType.Vertical,
                    CssClass = "btn-group-vertical",
                    GroupItems = new List<LoginMenuModel.GroupItem>()
                    {
                        #region Click Through and Social login

                        new LoginMenuModel.GroupItem()
                        {
                            CssClass = "btn-group-vertical",
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.ClickThrough,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "onSubmit(8)",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Tiếp tục",
                                    CssClass = "btn btn-success control-item-vertical col-sm-12",
                                    Css =
                                        "background: #74bf04; border: 1px solid #74bf04;color: #fff;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            CssClass = "btn-group-vertical",
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    CssClass = "btn btn-success control-item-vertical col-sm-12",
                                    Css =
                                        "background: #3b5998; border: 1px solid #3b5998; color: #fff;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            CssClass = "btn-group-vertical",
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.GooglePlus,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginGoogleApp()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Google Plus",
                                    CssClass = "btn btn-success control-item-vertical col-sm-12",
                                    Css =
                                        "background: red; border: 1px solid #ff0000; color: white;"
                                }
                            }
                        },

                        #endregion

                        #region Password Login

                        new LoginMenuModel.GroupItem()
                        {
                            CssClass = "btn-group-vertical",
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Password,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Mật khẩu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical",
                                    Id = "FixedPassword",
                                    Name = "PasswordField"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "validatePassword(\'1\', \'FixedPassword\')",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Đăng nhập",
                                    CssClass = "btn btn-success control-item-vertical",
                                    Css =
                                        "background: #74bf04; border: 1px solid #74bf04;color: #fff;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            CssClass = "btn-group-vertical",
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.OneTimePassword,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Mật khậu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical",
                                    Id = "OneTimePassword",
                                    Name = "PasswordField"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "validatePassword(\'2048\', \'OneTimePassword\')",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Đăng nhập",
                                    CssClass = "btn btn-success control-item-vertical",
                                    Css =
                                        "background: #74bf04; border: 1px solid #74bf04;color: #fff;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            CssClass = "btn-group-vertical",
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.RandomPassword,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = 3,
                                    PlaceHolder = "Your password",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical",
                                    Id = "PasswordRandom",
                                    Name = "PasswordField"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "validatePassword(\'32\', \'PasswordRandom\')",
                                    Type = 1,
                                    Value = "Random Password",
                                    CssClass = "btn btn-success control-item-vertical",
                                    Css =
                                        "background: #74bf04; border: 1px solid #74bf04;color: #fff;"
                                }
                            }
                        },

                        #endregion

                        #region Account With Pass

                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithPass,
                            CssClass = "btn-group-vertical login-box",
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Tên tài khoản",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical col-sm-12",
                                    Id = "UserName",
                                    Name = "UserName"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Mật khẩu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical col-sm-12",
                                    Id = "PasswordJson",
                                    Name = "PasswordJson"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginAccountWithPass()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Đăng nhập",
                                    Text = "Đăng nhập",
                                    CssClass = "btn btn-success control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "background-color: dodgerblue;color: #ffffff; float: left;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "openRegisterForm()",
                                    Type = (int) LoginMenuModel.MenuControlType.Href,
                                    Value = "Đăng ký",
                                    Text = "Đăng ký",
                                    CssClass = "control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "color: cornflowerblue;"
                                }
                            }
                        },

                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithPass,
                            CssClass = "btn-group-vertical register-box-general-form",
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Tài khoản",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    IsRequired = true,
                                    Id = "rUsername"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Họ và tên",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rName",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Mật khẩu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rPassword",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Nhập lại mật khẩu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rConfirmPassword",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    PlaceHolder = "Địa chỉ email",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rEmail",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Số điện thoại",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rPhone",
                                    IsRequired = true,
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Tổ chức",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rOrganization",
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Nghề nghiệp",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rJob"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Chức vụ",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical col-sm-12",
                                    Id = "rPosition"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.DropdownList,
                                    Id = "rAttribute1",
                                    Value="Startup|SME|NGO|NPO|Community|Đơn vị nhà nước|Trường/Viện|Một loại hình khác" ,
                                    Text  = "Loại hình tổ chức",
                                    CssClass = "control-item-vertical dropdownlist-item-vertical col-sm-12"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.DropdownList,
                                    Id = "rAttribute2",
                                    Value="ICT (Iot, software, fintech, edtech,...)|Nông nghiệp|Hóa sinh dược nhựa cao su|Cơ khí - điện tử - tư động hóa|Khác",
                                    Text  = "Ngành nghề doanh nghiệp",
                                    CssClass = "control-item-vertical dropdownlist-item-vertical col-sm-12"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.DropdownList,
                                    Id = "rAttribute3",
                                    Value="Không thuộc giai đoạn nào|Giai đoạn 1: ý tưởng|Giai đoạn 2: thử nghiệm sản phẩm|Giai đoạn 3: phát triển sản phẩm và mở rộng thị trường|Giai đoạn 4: tăng trưởng" ,
                                    Text  = "Giai đoạn phát triển",
                                    CssClass = "control-item-vertical dropdownlist-item-vertical col-sm-12"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "submitRegister()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Chấp nhận",
                                    Text = "Chấp nhận",
                                    CssClass = "btn btn-success control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "background-color: dodgerblue;color: #ffffff;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "cancelRegisterForm()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Hủy",
                                    Text = "Hủy",
                                    CssClass = "btn btn-success control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "background-color: red;color: #ffffff;margin-top: 0.4px;"
                                }
                            }
                        }

                        #endregion
                    }
                };
                return loginMenu;
            }
            else if (TypeOfMenu == (int)LoginMenuModel.MenuType.Horizontal)
            {
                LoginMenuModel loginMenu = new LoginMenuModel()
                {
                    InlineCss =
                        "font-family: roboto; position: absolute;max-width:35%;width:35%;top:0; left:0;margin-right: 40%; padding: 10px;text-align: center;color: #fff;border-radius: 0 0 4px 4px;background-color: #000;background-color: rgba(0,0,0,.5);animation: playMargin 1.5s;opacity: 100;",
                    X = 500,
                    Y = 300,
                    CssClass = "btn-group btn-group-justified",
                    GroupItemsType = (int)LoginMenuModel.MenuType.Horizontal,

                    GroupItems = new List<LoginMenuModel.GroupItem>()
                    {
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
//                            CssClass = "btn-group btn-group-justified",
                            LoginModeCode = (int) LoginModeEnum.ClickThrough,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "onSubmit(8)",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Css =
                                        "font-family: roboto; background: #74bf04;border: 1px solid #74bf04;text-align: center;font-size: 32px;font-weight: 700;position: relative;box-sizing: border-box;padding: 0 20px;color: #fff;text-shadow: none;line-height: 60px;height: 60px;transition: background .3s,color .3s;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
//                            CssClass = "btn-group btn-group-justified",
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Css =
                                        "font-family: roboto;background: #3b5998;border: 1px solid #74bf04;text-align: center;font-size: 32px;font-weight: 700;position: relative;box-sizing: border-box;padding: 0 20px;color: #fff;border: none;text-shadow: none;line-height: 60px;height: 60px;transition: background .3s,color .3s;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.GooglePlus,
//                            CssClass = "btn-group btn-group-justified",
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginGoogleApp()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Google Plus",
                                    Css =
                                        "font-family: roboto;background: red;border: 1px solid #74bf04;text-align: center;font-size: 32px;font-weight: 700;position: relative;box-sizing: border-box;padding: 0 20px;color: white;border: none;text-shadow: none;line-height: 60px;height: 60px;transition: background .3s,color .3s;"
                                }
                            }
                        }
                    }
                };
                return loginMenu;
            }
            else
            {
                LoginMenuModel loginMenu = new LoginMenuModel()
                {
                    GroupItems = new List<LoginMenuModel.GroupItem>()
                    {
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            DeviceType = (int) DeviceTypeEnum.PC,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Id = "btnfbdesktop",
                                    Css =
                                        "margin-left: 42.7%; margin-top: 23.3%; height: 4.2%; width: 12.2%; opacity: 1; border-radius: 11px; border-color: transparent; background: transparent;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithOutPass,
                            DeviceType = (int) DeviceTypeEnum.PC,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    Value = "",
                                    Id = "EnteredEmailAddressDesktop",
                                    Name = "EnteredEmailAddressDesktop",
                                    Css =
                                        "margin-left: 42.5%; margin-top: 5.4%; height: 4.5%; max-width: 37%; opacity: 1; border-color: transparent; font-size: 40px; text-align: center; color: black;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "enterEmailSubmit()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Id = "btnwdesktop",
                                    Css =
                                        "margin-left: 42.8%; margin-top: 1.8%; height: 4%; width: 12.5%; opacity: 1; border-radius: 14px; border-color: transparent; background: transparent;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            DeviceType = (int) DeviceTypeEnum.Mobile,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Id = "btnfbmobile",
                                    Css =
                                        "margin-left: 1.8%; margin-top: 66.6%; height: 4.6%; width: 31.6%; opacity: 0; border-radius: 13px;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithOutPass,
                            DeviceType = (int) DeviceTypeEnum.Mobile,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    Value = "",
                                    Id = "EnteredEmailAddressMb",
                                    Name = "EnteredEmailAddressMb",
                                    Css =
                                        "margin-left: 4%; margin-top: 12.9%; height: 4.5%; max-width: 52%; opacity: 1; border-radius: 9px; font-size: 40px; text-align: center; color: black;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "enterEmailSubmit()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Id = "btnwmobile",
                                    Css =
                                        "margin-left: 4.3%; margin-top: 2.6%; height: 4.7%; width: 31.8%; opacity: 0; border-radius: 16px;"
                                }
                            }
                        }
                    }
                };
                return loginMenu;
            }
        }

        public static LoginMenuModel GenerateLoginMenuModelLusineOldDesign(int TypeOfMenu)
        {
            LoginMenuModel loginMenu = new LoginMenuModel()
            {
                GroupItems = new List<LoginMenuModel.GroupItem>()
                    {
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            DeviceType = (int) DeviceTypeEnum.PC,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Id = "btnfbdesktop",
                                    Css =
                                        "margin-left: 42.7%; margin-top: 23.3%; height: 4.2%; width: 12.2%; opacity: 1; border-radius: 11px; border-color: transparent; background: transparent;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithOutPass,
                            DeviceType = (int) DeviceTypeEnum.PC,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    Value = "",
                                    Id = "EnteredEmailAddressDesktop",
                                    Name = "EnteredEmailAddressDesktop",
                                    Css =
                                        "margin-left: 42.5%; margin-top: 5.4%; height: 4.5%; max-width: 37%; opacity: 1; border-color: transparent; font-size: 40px; text-align: center; color: black;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "enterEmailSubmit()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Id = "btnwdesktop",
                                    Css =
                                        "margin-left: 42.8%; margin-top: 1.8%; height: 4%; width: 12.5%; opacity: 1; border-radius: 14px; border-color: transparent; background: transparent;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            DeviceType = (int) DeviceTypeEnum.Mobile,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Id = "btnfbmobile",
                                    Css =
                                        "margin-left: 1.8%; margin-top: 66.6%; height: 4.6%; width: 31.6%; opacity: 0; border-radius: 13px;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithOutPass,
                            DeviceType = (int) DeviceTypeEnum.Mobile,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    Value = "",
                                    Id = "EnteredEmailAddressMb",
                                    Name = "EnteredEmailAddressMb",
                                    Css =
                                        "margin-left: 4%; margin-top: 12.9%; height: 4.5%; max-width: 52%; opacity: 1; border-radius: 9px; font-size: 15px; text-align: center; color: black;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "enterEmailSubmit()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Id = "btnwmobile",
                                    Css =
                                        "margin-left: 4.3%; margin-top: 2.6%; height: 4.7%; width: 31.8%; opacity: 0; border-radius: 16px;"
                                }
                            }
                        }
                    }
            };
            return loginMenu;
        }

        public static LoginMenuModel GenerateLoginMenuModelLusineNewDesign(int TypeOfMenu)
        {
            LoginMenuModel loginMenu = new LoginMenuModel()
            {
                GroupItems = new List<LoginMenuModel.GroupItem>()
                    {
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            DeviceType = (int) DeviceTypeEnum.PC,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Id = "btnfbdesktop",
                                    Css =
                                        "position: absolute; margin-left: -8.8%; margin-top: 22%; height: 6%; width: 17.5%; opacity: 1; border-radius: 0px; border: 0px; border-color: transparent; background: transparent;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithOutPass,
                            DeviceType = (int) DeviceTypeEnum.PC,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    Value = "",
                                    Id = "EnteredEmailAddressDesktop",
                                    Name = "EnteredEmailAddressDesktop",
                                    Css =
                                        "position: absolute; margin-left: -16%; margin-top: 34%; height: 6.5%; max-width: 31.5%; opacity: 1; border-color: transparent; font-size: 40px; text-align: center; color: white;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "enterEmailSubmit()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Id = "btnwdesktop",
                                    Css =
                                        "margin-left: -8.9%; margin-top: 38.6%; height: 6.7%; width: 17.8%; opacity: 1; border-radius: 0px; border: 0px; border-color: transparent; background: transparent; position: absolute;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.Facebook,
                            DeviceType = (int) DeviceTypeEnum.Mobile,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginFacebook()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Facebook",
                                    Id = "btnfbmobile",
                                    Css =
                                        "margin-left: 1.5%; margin-top: 42.6%; height: 6.8%; width: 50%; opacity: 0; border-radius: 0px;"
                                }
                            }
                        },
                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithOutPass,
                            DeviceType = (int) DeviceTypeEnum.Mobile,
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    Value = "",
                                    Id = "EnteredEmailAddressMb",
                                    Name = "EnteredEmailAddressMb",
                                    Css =
                                        "margin-left: 1%; margin-top: 20.5%; height: 4.5%; max-width: 87%; opacity: 1; border-radius: 9px; font-size: 15px; text-align: center; color: white;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "enterEmailSubmit()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Continue",
                                    Id = "btnwmobile",
                                    Css =
                                        "margin-left: 0%; margin-top: 1.5%; height: 7%; width: 52%; opacity: 0; border-radius: 0px;"
                                }
                            }
                        }
                    }
            };
            return loginMenu;
        }
        public static LoginMenuModel TestGenerateLoginMenu(int TypeOfMenu)
        {
            LoginMenuModel loginMenu = new LoginMenuModel()
            {
                Id = "general-form",
                InlineCss =
                         "font-family: roboto; position: relative; width:35%; top:0; left:0; padding: 10px;text-align: center;color: #fff;border-radius: 0 0 4px 4px;background-color: #000;background-color: rgba(0,0,0,.5);animation: playMargin 1.5s;opacity: 100;",
                X = 32.5,
                Y = 7.9,
                GroupItemsType = (int)LoginMenuModel.MenuType.Vertical,
                CssClass = "btn-group-vertical",
                GroupItems = new List<LoginMenuModel.GroupItem>()
                    {
                        #region Account With Pass

                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithPass,
                            CssClass = "btn-group-vertical login-box",
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Username",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical col-sm-12",
                                    Id = "UserName",
                                    Name = "UserName"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Password",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical col-sm-12",
                                    Id = "PasswordJson",
                                    Name = "PasswordJson"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "loginAccountWithPass()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Login",
                                    Text = "Login",
                                    CssClass = "btn btn-success control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "background-color: dodgerblue;color: #ffffff; float: left;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "openRegisterForm()",
                                    Type = (int) LoginMenuModel.MenuControlType.Href,
                                    Value = "Register",
                                    Text = "Register",
                                    CssClass = "control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "color: cornflowerblue;"
                                }
                            }
                        },

                        new LoginMenuModel.GroupItem()
                        {
                            Css = "",
                            LoginModeCode = (int) LoginModeEnum.AccountWithPass,
                            CssClass = "btn-group-vertical register-box-general-form",
                            ControlItems = new List<LoginMenuModel.ControlItem>()
                            {
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Tài khoản",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    IsRequired = true,
                                    Id = "rUsername"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Họ và tên",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rName",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Mật khẩu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rPassword",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Password,
                                    PlaceHolder = "Nhập lại mật khẩu",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rConfirmPassword",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.Email,
                                    PlaceHolder = "Địa chỉ email",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rEmail",
                                    IsRequired = true
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Số điện thoại",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rPhone",
                                    IsRequired = true,
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Tổ chức",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rOrganization",
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Nghề nghiệp",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass =
                                        "control-item-vertical input-item-vertical register-inputs-vertical col-sm-12",
                                    Id = "rJob"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.TextBox,
                                    PlaceHolder = "Chức vụ",
                                    Css =
                                        "border-bottom-width: 3.5px;",
                                    CssClass = "control-item-vertical input-item-vertical col-sm-12",
                                    Id = "rPosition"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.DropdownList,
                                    Id = "rAttribute1",
                                    Value="Startup|SME|NGO|NPO|Community|Đơn vị nhà nước|Trường/Viện|Một loại hình khác" ,
                                    Text  = "Loại hình tổ chức",
                                    CssClass = "control-item-vertical dropdownlist-item-vertical col-sm-12"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.DropdownList,
                                    Id = "rAttribute2",
                                    Value="ICT (Iot, software, fintech, edtech,...)|Nông nghiệp|Hóa sinh dược nhựa cao su|Cơ khí - điện tử - tư động hóa|Khác",
                                    Text  = "Ngành nghề doanh nghiệp",
                                    CssClass = "control-item-vertical dropdownlist-item-vertical col-sm-12"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Type = (int) LoginMenuModel.MenuControlType.DropdownList,
                                    Id = "rAttribute3",
                                    Value="Không thuộc giai đoạn nào|Giai đoạn 1: ý tưởng|Giai đoạn 2: thử nghiệm sản phẩm|Giai đoạn 3: phát triển sản phẩm và mở rộng thị trường|Giai đoạn 4: tăng trưởng" ,
                                    Text  = "Giai đoạn phát triển",
                                    CssClass = "control-item-vertical dropdownlist-item-vertical col-sm-12"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "submitRegister()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Chấp nhận",
                                    Text = "Chấp nhận",
                                    CssClass = "btn btn-success control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "background-color: dodgerblue;color: #ffffff;"
                                },
                                new LoginMenuModel.ControlItem()
                                {
                                    Onclick = "cancelRegisterForm()",
                                    Type = (int) LoginMenuModel.MenuControlType.Button,
                                    Value = "Hủy",
                                    Text = "Hủy",
                                    CssClass = "btn btn-success control-item-vertical login-buttons-vertical col-sm-12",
                                    Css =
                                        "background-color: red;color: #ffffff;margin-top: 0.4px;"
                                }
                            }
                        }

                        #endregion
                    }
            };
            return loginMenu;
        }


    }
}