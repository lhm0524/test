/// <reference path="jquery.min.js" />

function LoadAlldeparent() {

}
function BindEvent() {
    var nodes_a = document.querySelectorAll('section a');
    console.info(nodes_a.length);
    for (var i = 0; i < nodes_a.length; i++) {
        nodes_a[i].addEventListener('click', function (event) {
            var par = this.parentNode.parentNode.parentNode.parentNode;
            var text = par.querySelector('header').textContent.trim();
            var d = this.getAttribute('href');
            d += '&name=' + encodeURI(text);
            this.setAttribute('href', d);
            console.info(event.currentTarget)
        });//test
    }
}//绑定a标签的事件并设置其href属性

function LoadDepartment() {
    var nodes = document.querySelectorAll('section');
    var i = 0;
    for (i = 0; i < nodes.length; i++) {
        LoadData(nodes[i], i);
    }
    //BindEvent();
}
function LoadData(element, i) {
    $.post('/Category/LoadDepartmentByParentID/', { id: element.dataset['id'] }, function (data) {
        var ul = element.querySelector('ul');
        ul.innerHTML = '';
        var li = document.createElement('li');
        li.dataset['id'] = '0';
        var a = document.createElement('a');
        a.innerText = '全部';
        li.addEventListener('click', BindEvent);
        li.appendChild(a);
        ul.appendChild(li);
        for (var j = 0; j < data.length; j++) {
            var li = document.createElement('li');
            li.dataset['id'] = data[j].Id;
            var a = document.createElement('a');
            a.innerText = data[j].Name;
            a.href = '/CategoryList/Index/' + element.dataset['id'] + '/' + encodeURI(a.innerText);
            li.addEventListener('click',BindEvent );
            li.appendChild(a);
            ul.appendChild(li);
        }
        var li = document.createElement('li');
        li.dataset['id'] = '-1';
        var a = document.createElement('a');
        a.innerText = '其他';
        li.addEventListener('click', BindEvent);
        li.appendChild(a);
        ul.appendChild(li);
    });
}
function BindEvent(e) {
    console.info(e.currentTarget);
}