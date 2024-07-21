function showVerticalMenu() {
    document.getElementById("menu-button").classList.toggle("open");
    document.getElementById("vertical-menu").classList.toggle("show");
}
function visitTelegram() {
    document.location.href = "https://t.me/+qce_aMn5dRk1NGIy";
}
function visitVk() {
    document.location.href = "https://t.me/+qce_aMn5dRk1NGIy";
}
function setClipboardButton(){
    var codeSnippets = document.querySelectorAll('pre');
    var index = 1;
    codeSnippets.forEach(function (codeSnippet)
    {
        var copyButton = document.createElement('button');
        copyButton.classList.toggle("copy-button");
        copyButton.textContent = "копировать";
        copyButton.id = index.toString();
        index+=1;
        codeSnippet.parentNode.insertBefore(copyButton, codeSnippet);
        var cbp = new ClipboardJS(copyButton, {
            target: function(trigger) {
                return trigger.nextElementSibling;
            }
        });
        cbp.on('success', function(e) {
            e.clearSelection();
            var cbs = document.getElementsByClassName('copy-button');
            for (var i = 0; i<cbs.length; i++) {
                var el = cbs[i];
                el.textContent = 'копировать';
            }
            copyButton.textContent = 'скопировано';
        });
    });
}
function makeMark(index) {
    var dropdowns = document.getElementsByClassName("item");
    for (var i = 0; i < dropdowns.length; i++) {
        var el = dropdowns[i];
        if (el.classList.contains("selected")) {
            el.classList.remove("selected");
        }

    }
    document.getElementById(index).classList.toggle("selected");
}
function clearCode() {
    var codes = document.querySelectorAll('code');
    codes.forEach(function (code) {
        code.removeAttribute('data-highlighted');
    });
    codes.forEach(function(code) {
        let text = code.textContent;
        code.innerHTML = "";
        code.textContent = text;
        code.classList.forEach(function (cls){
            if (cls.includes('hljs')) {
                code.classList.remove(cls);
            }
            else if (!cls.includes('language')) {
                code.classList.remove(cls);
            }
        });
    });
}
function changeTheme(theme) {
    if (theme === 'light') {
        setActiveTheme('dark');
    }
    else {
        setActiveTheme('light');
    }
}
function setActiveTheme(theme) {
    var darkThemeLink = document.getElementById("dark-theme");
    var lightThemeLink = document.getElementById("light-theme");
    darkThemeLink.disabled = (theme === 'light');
    lightThemeLink.disabled = (theme === 'dark');
    localStorage.setItem("theme", theme);
    hljs.highlightAll();
}
function loadTheme() {
    var currentTheme = localStorage.getItem("theme");
    if (!currentTheme) {
        setActiveTheme('dark');
    }
    else {
        setActiveTheme(currentTheme);
    }
}
function switchTheme() {
    clearCode();
    clearCode();
    changeTheme(localStorage.getItem("theme"));
}

function setAdv() {
    var div = document.createElement("div");
    div.id = "yandex_rtb_R-A-10026114-5";
    var script = document.createElement("script");
    script.text = `
        window.yaContextCb.push(()=>{
            Ya.Context.AdvManager.render({
                "blockId": "R-A-10026114-5",
                "renderTo": "yandex_rtb_R-A-10026114-5"
            })
        });
    `;
    var preElement = document.querySelector("pre");
    if (preElement) {
        preElement.parentNode.insertBefore(div, preElement);
        preElement.parentNode.insertBefore(script, preElement);
    }
}
window.onclick = function (event) {
    if (!event.target.matches('.menu-button') & !event.target.matches('.theme-switcher')) {
        document.getElementById("menu-button").classList.remove("open");
        var dropdowns = document.getElementsByClassName("vertical-menu-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}