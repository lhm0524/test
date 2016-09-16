/// <reference path="jquery.min.js" />
var urlid = location.href.substring(location.href.lastIndexOf('/') + 1);
var commitview = document.getElementById('commitview');
var pageindex = 1;
function BindClickEvent() {
    document.getElementById('btn_fabu').addEventListener('click', CreateComment);
    document.getElementById('loadmorelink').addEventListener('click', LoadMore);
}

function CreateComment(e) {
    var text = document.getElementById('replyarea').value;
    if (text.length == 0) {
        return;
    }
    console.info(new Date().getMinutes());
    var data = {};
    data.WordsBody = text;
    data.WeiKeId = urlid;
    $.post('/Comment/Create', data, function (result) {
        if (result.state == true) {
            alert('发表成功');
            document.getElementById('replyarea').value = '';
        } else if (result.state == 'empty') {
            alert('内容为空');
        } 
    })
}

function RefreshWords() {
    $.getJSON('AjaxHandle/WordsHandle.ashx?action=getMore&pageindex=3&weikeid=' + location.href.match('\\?id=(\\d+)')[1], null, function (result) {
        for (var i = 0; i < result.length; i++) {
            commitview.appendChild(createcommitElement(result[i]));
        }
    })

}//刷新评论列表

function createcommitElement(data) {
    //{imgpath = "", title = "", author = "", time="", body=""}
    var li = document.createElement('li');
    var article = document.createElement('acticle');
    article.className = 'am-comment';
    li.style = "margin-right: 2px;margin-bottom: 3px;";
    var imga = document.createElement('a');
    var img = document.createElement('img');
    img.className = 'am-comment-avatar';
    img.src = data.Img;
    img.width = 48;
    img.height = 48;
    imga.appendChild(img);
    article.appendChild(imga);
    var maindiv = document.createElement('div');
    maindiv.className = 'am-comment-main';
    var header = document.createElement('header');
    header.className = 'am-comment-hd';
    var title = document.createElement('h3');
    title.className = 'am-comment-title';
    title.innerText = data.WordsTitle;
    var divmeta = document.createElement('div');
    divmeta.className = 'am-comment-meta';
    var metaa = document.createElement('a');
    metaa.className = 'am-comment-author';
    metaa.innerText = data.UserName;//data.author;
    divmeta.appendChild(metaa);
    divmeta.innerText += ' 评论于 ';/////////////////////////////////////////////////
    var time = document.createElement('span');
    time.innerText = formatTime(data.WordsTime, 'yyyy-MM-dd hh:mm:ss');//data.Time;
    divmeta.appendChild(time);
    header.appendChild(divmeta);
    var divbody = document.createElement('div');
    divbody.className = 'am-comment-bd';
    divbody.innerText = data.WordsBody;
    maindiv.appendChild(header);
    maindiv.appendChild(divbody);
    article.appendChild(maindiv);
    li.appendChild(article);
    return li;
} //创建评论节点元素

function LoadMore(e) {
    var getdata = {};
    //pagesize=2&pageindex=1&order=wordstime&isasc=true&id=667
    getdata.pagesize = 2;
    getdata.pageindex = pageindex++;
    getdata.order = "wordstime";
    getdata.isasc = true;
    getdata.id = urlid;
    $.post('/Play/LoadCommit/', getdata, function (result) {
        AppendCommint(result);
    });
}

function formatTime(val, formtstring) {
    var re = /-?\d+/;
    var m = re.exec(val);
    var d = new Date(parseInt(m[0]));
    //console.info(d);
    //return d.format(formtstring);
    return d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate() + ' ' + d.getHours() + ':' + d.getMinutes();
}//反序列化日期

function LoadCommit() {
    var getdata = {};
    //pagesize=2&pageindex=1&order=wordstime&isasc=true&id=667
    getdata.pagesize = 2;
    getdata.pageindex = pageindex++;
    getdata.order = "wordstime";
    getdata.isasc = true;
    getdata.id = urlid;
    var commitview = document.getElementById('commitview');

    $.post('/Play/LoadCommit/', getdata, function (result) {
        AppendCommint(result);
        //加载第一页
        if (pageindex <= result.count) {
            getdata.pageindex = pageindex++;
            $.post('/Play/LoadCommit/', getdata, function (d) {
                AppendCommint(d);
            });
        }
    });
}//异步加载前两页评论

function AppendCommint(result) {
    for (var i = 0; i < result.list.length; i++) {
        var li = createcommitElement(result.list[i]);
        var listli = commitview.querySelectorAll('li');
        if (listli.length == 0) {
            commitview.appendChild(li);
        } else {
            commitview.insertBefore(li, listli[0]);
        }
    }
}//添加评论集合