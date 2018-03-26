(function ($) {

	$(document).ready(function() {
 $(".addToCartButton").click(function () {
			
			var title, message, opts, container;
			
			title = "Đã thêm vào giỏ hàng !";
			message = "Vào giỏ hàng của bạn để xem chi tiết.";

			opts = {};
			
			opts.classes = ["gray"];
			container = "#freeow-tr";

			
			opts.hideStyle = {
			    opacity: 0,
			    left: "400px"
			};
			opts.showStyle = {
			    opacity: 1,
			    left: 0
			};

			$(container).freeow(title, message, opts);
			
		});
	  

		$(".deleteCartButton").click(function () {
			
			var title, message, opts, container;
			
			title = "Đã xóa sản phẩm !";
			message = "Hãy kiểm tra chi tiết lại giỏ hàng.";

			opts = {};
			
			opts.classes = ["gray"];
			container = "#freeow-tr";

			
			opts.hideStyle = {
			    opacity: 0,
			    left: "400px"
			};
			opts.showStyle = {
			    opacity: 1,
			    left: 0
			};

			$(container).freeow(title, message, opts);
			
		});
		
		$(".updateCartButton").click(function () {
			
			var title, message, opts, container;
			
			title = "Đã cập nhật giỏ hàng !";
			message = "Hãy kiểm tra chi tiết lại giỏ hàng.";

			opts = {};
			
			opts.classes = ["gray"];
			container = "#freeow-tr";

			
			opts.hideStyle = {
			    opacity: 0,
			    left: "400px"
			};
			opts.showStyle = {
			    opacity: 1,
			    left: 0
			};

			$(container).freeow(title, message, opts);
			
		});
		
	});

}(jQuery));
