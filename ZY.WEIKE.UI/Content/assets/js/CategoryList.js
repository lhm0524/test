//{ Id: 105, Description: '我是简要描述', Name: 'C#基础', VideoImgPath:'sss'};
var h2_falg = document.getElementById('h2_falg');
var ul = document.getElementById('list');
var ulnodes = ul.getElementsByTagName('li');//二级分类下的li元素
var courseList = document.querySelectorAll('#list ul li');//每个视频对应的div元素
var pageindex = 1;
function AddWeiKeEntity(data) {
    var li = document.createElement('li');
    li.dataset.Id = data.Id;
    li.className = 'am-g am-list-item-desced am-list-item-thumbed am-list-item-thumb-left';

    var divpic = document.createElement('div');
    divpic.className = 'am-u-sm-4 am-list-thumb';
    var a = document.createElement('a');
    var img = document.createElement('img');
    img.src = '/Home/ImgPath/' + data.Id;//'Users/' + data.VideoImgPath;
    a.appendChild(img);
    divpic.appendChild(a);

    var divbody = document.createElement('div');
    divbody.className = 'am-u-sm-8 am-list-main';
    var h3 = document.createElement('h3');
    h3.className = 'am-list-item-hd';
    var h3_a = document.createElement('a');
    h3_a.href = '/Play/Index/' + data.Id;
    h3_a.innerText = data.Name;
    h3.appendChild(h3_a);
    var divbody_div = document.createElement('div');
    divbody_div.className = 'am-list-item-text';
    divbody_div.innerText = data.Description;
    divbody.appendChild(h3);
    divbody.appendChild(divbody_div);

    li.appendChild(divpic);
    li.appendChild(divbody);
    return li;
} //数据添加实例

function Activity() {
    var url = window.location.href;
    var pattern = new RegExp(/(\d+)\//);
    var patternname = new RegExp(/(%.+)/);
    var name = url.match(/(%.+)/)[1];
    if (patternname.test(url)) {
        document.getElementById('am-title').innerText = decodeURI(name);
    }
    if (pattern.test(url)) {
        for (var i = 0; i < ulnodes.length; i++) {
            var div = ulnodes[i].querySelector('div');
            if (decodeURI(name) == div.textContent) {
                div.className = 'weike-active';
                h2_falg.innerText = div.textContent;
            }
        }
    } else {
        var div = ulnodes[0].querySelector('div');
        div.className = 'weike-active';
        h2_falg.innerText = div.textContent;//
    }
}//激活状态

function Check(str) {
    for (var i = 0; i < ulnodes.length; i++) {
        var div = ulnodes[i].querySelector('div');
        if (str != div.dataset.typeid) {
            div.className = '';
        }
    }
}//去掉激活状态

function BindClickEvent() {
    document.getElementById('load_more').addEventListener('click', loadmore);
    for (var i = 0; i < ulnodes.length; i++) {
        var li = ulnodes[i];
        var div = li.querySelector('div');
        div.addEventListener('click', SecondaryDiv);
    }
} //绑定事件

function AddWeiKeList(data, flag) {
    var list_Course = document.getElementById('list_Course');
    if (flag == true) {
        list_Course.innerHTML = '';
    }
    for (var i = 0; i < data.length; i++) {
        var li = AddWeiKeEntity(data[i]);
        list_Course.appendChild(li);
    }
} //添加集合

function LoadingEntities() {
    var typeid = document.getElementsByClassName('weike-active')[0].dataset['typeid'];
    $.post('/CategoryList/GetPageCount/page/', { type: typeid, pagesize: 4 }, function (count) {
        $.post('/CategoryList/LoadPageEntities/page/', { type: typeid, pageindex: pageindex++, pagesize: 4 }, function (jsondata) {
            AddWeiKeList(jsondata.data, true);
        });
    });
} //如果有前两页的话 则加载

function loadmore(e) {
    var typeid = document.getElementsByClassName('weike-active')[0].dataset['typeid'];
    $.post('/CategoryList/GetPageCount/page/', { type: typeid, pagesize: 4 }, function (data) {
        if (pageindex <= data) {
            $.post('/CategoryList/LoadPageEntities/page/', { type: typeid, pageindex: pageindex++, pagesize: 4 }, function (jsondata) {
                AddWeiKeList(jsondata.data, false);
            });
        }
    });
} //加载更多

function SecondaryDiv(e)
{
    if (e.currentTarget.className == 'weike-active') {
        //this.className = '';
    } else {
        h2_falg.innerText = this.textContent;
        this.className = 'weike-active';
    }
    Check(e.currentTarget.dataset.typeid);

    var typeid = parseInt(e.currentTarget.dataset.typeid);
    switch (typeid) {
        case 0:
            //加载全部
            break;
        case -1:
            //加载其他
            break;
        default:
            $.post('/CategoryList/LoadWeiKeEntities/', { id: e.currentTarget.dataset.typeid }, function (data) {
                pageindex = 1;
                LoadingEntities();
            });
            break;
    }
} //二级分类的点击事件