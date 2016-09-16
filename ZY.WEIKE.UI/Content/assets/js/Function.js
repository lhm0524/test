/**
 * Created by xfy11 on 2016/6/24.
 */
function MyAjax(method, data, url, callback) {
    var xhr;
    if (XMLHttpRequest){
        xhr = new XMLHttpRequest();
    } else {
        xhr = new ActiveXObject('MircroSoft.XMLHttp');
    }
    if (method.toLowerCase(method) == 'post') {
        xhr.open(method, url, true);
        xhr.setRequestHeader("Context-type", "application/x-www-form-urlencoded");
        xhr.send(data);
    } else {
        xhr.open(method, url + "?" + data, true);
        xhr.send();
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            callback(xhr.responseText);
        }
    }
}