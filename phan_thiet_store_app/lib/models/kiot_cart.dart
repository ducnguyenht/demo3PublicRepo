import './cart.dart';

class KiotCart {
  List<String> dsIdHangHoa;
  String name;
  String phone;
  String email;
  String address;
  String note;
  String strCupon;

  KiotCart({this.dsIdHangHoa,
    this.name,
    this.phone,
    this.email,
    this.address,
    this.note,
    this.strCupon});

  factory KiotCart.fromCart(String name, String phone, String email, String address, Cart cart) {
    var ret = new KiotCart();
    ret.name = name;
    ret.phone = phone;
    ret.email = email;
    ret.address = address;
    ret.dsIdHangHoa = new List<String>();

    for (var item in cart.items) {
      for (var itemCount = 0; itemCount < item.quantity; itemCount++) {
        ret.dsIdHangHoa.add(item.id);
      }
    }

    return ret;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['dsIdHangHoa'] = this.dsIdHangHoa;
    data['name'] = this.name;
    data['phone'] = this.phone;
    data['email'] = this.email;
    data['address'] = this.address;
    data['note'] = this.note;
    data['strCupon'] = this.strCupon;
    return data;
  }
}