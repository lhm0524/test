var span = document.getElementById('sexbar').childNodes.item(1);
var targetelement;
function BindEvent() {
    document.getElementById('tr_man').addEventListener('click', function (e) {
        document.getElementById('man').checked = true;
    });
    document.getElementById('tr_woman').addEventListener('click', function (e) {
        document.getElementById('woman').checked = true;
    });
    $("#sexbar").on('click', function () {
        $('#sex-MODEL').MODEL({
            onConfirm: function () {
                span.textContent = document.getElementById('man').checked == true ? '男' : '女';
            }
        });
    });
    document.getElementById('btn_logout').addEventListener('click', Logout);
    document.getElementById('email').addEventListener('click', function (e) {
        targetelement = e.currentTarget;
        MODELsetting('邮箱');
    });
    document.getElementById('username').addEventListener('click', function (e) {
        targetelement = e.currentTarget;
        MODELsetting('用户名');
    });
    document.getElementById('birthday').addEventListener('click', function (e) {
        targetelement = e.currentTarget;
        MODELsetting('出生日期');
        //e.target.
    });
    document.getElementById('btn_ensure').addEventListener('click', btnensure);

    document.getElementById('btn_saveentity').addEventListener('click', btnsave);
}


function MODELsetting(text) {
    var edittitle = document.getElementById('edittitle');
    edittitle.textContent = text;
    document.getElementById('edittext').setAttribute('type', 'text');
    if (text == '邮箱') {
        edittitle.dataset['target'] = 'email';
    } else if (text == '用户名') {
        edittitle.dataset['target'] = 'username';
    } else {
        edittitle.dataset['target'] = 'birthday';
        document.getElementById('edittext').setAttribute('type', 'date');
    }
    //return;
    document.getElementById('edittext').value = targetelement.querySelector('span').textContent;
    $('#editMODEL').MODEL('open');
}

function Logout(e) {
    $.post('/User/Logout', null, function (data) {
        if(data.state == "succeed") {
            window.location = "/User/Login";
        }
    })
}

function btnensure(e) {
    console.info(e.currentTarget);
    targetelement.querySelector('span').textContent = document.getElementById('edittext').value;
}

function btnsave(e) {
    var data = {};
    var sex = document.getElementById('sexbar').querySelector('span').textContent;
    var email = document.getElementById('email').querySelector('span').textContent;
    var username = document.getElementById('username').querySelector('span').textContent;
    var birthday = document.getElementById('birthday').querySelector('span').textContent;
    var anwser = document.getElementById('anwser').value;
    var oldpwd = document.getElementById('oldpwd').value;
    if (oldpwd.length != 0) {
        if (document.getElementById('newpwd').value == document.getElementById('newpwdx').value) {
            data.oldpwd = oldpwd;
            data.UserPwd = document.getElementById('newpwd').value;
        }
    }
    data.Sex = sex == '男' ? 1 : 0;
    data.Email = email;
    data.UserName = username;
    data.Birthday = birthday;
    data.Answer = anwser;
    $.post('/user/EditEntity/', data, function (data) {
        if (data.state == 'succeed') {
            alert('修改成功!');
        } else {
            alert(data.msg);
        }
    })
}