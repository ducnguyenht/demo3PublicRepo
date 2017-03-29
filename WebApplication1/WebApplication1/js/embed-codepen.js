angular.module("embedCodepen.config", []).value("embedCodepen.config", {
    debug: !0
}), angular.module("embedCodepen.directives", []), angular.module("embedCodepen", ["embedCodepen.config", "embedCodepen.directives"]),
    function () {
        function e() {
            return {
                restrict: "A",
                scope: {
                    themeId: "@",
                    slugHash: "@",
                    user: "@",
                    defaultTab: "@",
                    height: "@",
                    showTabBar: "@",
                    animations: "@",
                    border: "@",
                    borderColor: "@",
                    tabBarColor: "@",
                    tabLinkColor: "@",
                    activeTabColor: "@",
                    activeLinkColor: "@",
                    linkLogoColor: "@",
                    "class": "@",
                    customCssUrl: "@"
                },
                template: ["<iframe ", 'scrolling="no" ', 'frameborder="0" ', 'class="cp_embed_iframe" ', 'allowtransparency="true" ', 'allowfullscreen="true" ', 'style="width: 100%; overflow: hidden;"', ">", "</iframe>"].join(""),
                link: function (e, o) {
                    if (e.slugHash && e.user) {
                        var r = document.location.protocol + "//codepen.io/" + e.user + "/embed/" + e.slugHash + "?user=" + e.user,
                            a = ["themeId", "defaultTab", "height", "showTabBar", "animations", "border", "borderColor", "tabBarColor", "tabLinkColor", "activeTabColor", "activeLinkColor", "linkLogoColor", "class", "customCssUrl"];
                        angular.forEach(a, function (o) {
                            e[o] && (r += "&" + o + "=" + e[o])
                        }), o.find("iframe").attr("src", r).attr("height", e.height).attr("id", "cp_embed_" + e.slugHash)
                    }
                }
            }
        }
        e.$inject = [], angular.module("embedCodepen.directives").directive("slugHash", e)
    }();