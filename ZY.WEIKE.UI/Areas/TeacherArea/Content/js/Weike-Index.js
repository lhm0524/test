/// <reference path="E:\工作室\MVC\ZY.WEIKE\ZY.WEIKE.UI\Content/assets/js/jquery.min.js" />
/// <reference path="E:\工作室\MVC\ZY.WEIKE\ZY.WEIKE.UI\Content/assets/js/amazeui.min.js" />

function AppendDepartment(Id) {
    var select = document.getElementById('select_department');
    select.innerHTML = '';
    var all = document.createElement('option');
    all.innerText = '全部';
    all.value = 0;
    select.appendChild(all);
    $.post('/Weike/LoadDepartment', { parentId: Id }, function (jsondata) {
        for (var i = 0; i < jsondata.length; i++) {
            var option = document.createElement('option');
            option.innerText = jsondata[i].Name;
            option.value = jsondata[i].Id;
            select.appendChild(option);
        }
    });
}

function BindEvent() {
    //$('.am-btn-toolbar div button').off('click');
    $('.am-btn-toolbar div button').off('click').on('click', function (e) {
        var Id = this.parentNode.dataset.Id;
        var Action = this.dataset.action;
        switch (Action) {
            case 'edit':
                break;
            case 'delete':
                document.getElementById('confirm').dataset.id = Id;
                $('#Modal-Delete').modal('open');
                break;
            default:
                break;
        }
    });

    $('#confirm').off('click').on('click', function (e) {
        var id = this.dataset.id;
        var data = {};
        data.id = id;
        //console.info(document.querySelectorAll('[data-id=' + id + ']'));
        //$.post('/Teacher/Weike/DeleteWeike', data, function (jsondata) {
        //    if (jsondata.state) {
        //        alert(jsondata.msg);
        //    } else {
        //        alert(jsondata.msg);
        //    }
        //});
    });

    $('.am-pagination li').off('click').on('click', liClickEvent);

    $('#search').off('click').on('click', search);
}

function liClickEvent(e) {
    console.info(e.currentTarget.dataset.pageindex);
}

function search(e) {
    var data = {};
    data.keyword = $('#keyword').val();
    data.pageIndex = 1;
    data.pageSize = 10;
    $.post('/Teacher/Weike/Search/', data, function (jsondata) {
        AppendToTbody(jsondata);
    });
}

function CreateTr(entity) {
    var tr = document.createElement('tr');
    var td0 = document.createElement('td');//选择框
    var input = document.createElement('input');
    input.type = 'checkbox';
    td0.appendChild(input);

    var td6 = document.createElement('td');//ID
    td6.innerText = entity.Id;

    var td1 = document.createElement('td');//名称
    var a = document.createElement('a');
    //a.href = '' + entity.Id;
    a.textContent = entity.Name;
    td1.appendChild(a);

    var td2 = document.createElement('td');//类别
    td2.innerText = entity.TypeId;

    var td3 = document.createElement('td');//创建日期
    td3.className = 'am-hide-sm-only';
    td3.innerText = ChangeDateFormat(entity.CreateTime);//entity.CreateTime;

    var td4 = document.createElement('td');
    var div = document.createElement('div');
    div.className = 'am-btn-toolbar';
    var div00 = document.createElement('div');
    div00.dataset.Id = entity.Id;
    div00.className = 'am-btn-group am-btn-group-xs';
    var button0 = document.createElement('button');
    button0.dataset.action = 'edit';
    button0.className = 'am-btn am-btn-default am-btn-xs am-text-secondary';
    var span00 = document.createElement('span');
    span00.className = 'am-icon-pencil-square-o';
    button0.appendChild(span00);
    button0.innerHTML += ' 编辑';
    var button1 = document.createElement('button');
    button1.dataset.action = 'delete';
    button1.className = 'am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only';
    var span01 = document.createElement('span');
    span01.className = 'am-icon-trash-o';
    button1.appendChild(span01);
    button1.innerHTML += ' 删除';
    div00.appendChild(button0);
    div00.appendChild(button1);
    div.appendChild(div00);
    td4.appendChild(div);

    tr.appendChild(td0);
    tr.appendChild(td6);
    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td3);
    tr.appendChild(td4);
    return tr;
}

function Init()
{
    $.post('/Teacher/Weike/Search', { pageIndex: 1, pageSize: 10, keyword: '' }, function (jsondata) {
        AppendToTbody(jsondata);
    });
}

function AppendToTbody(jsondata) {
    var data = jsondata.Data;
    for (var key in data) {
        tbody.appendChild(CreateTr(data[key]));
    }
    BindEvent();
}

function ChangeDateFormat(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    }
    else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }

    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate + ' ' + date.getHours() + ':' + date.getMinutes();
}