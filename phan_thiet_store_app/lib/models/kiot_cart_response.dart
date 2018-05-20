class KiotCartResponse {
  String code;

  KiotCartResponse({this.code});

  KiotCartResponse.fromJson(Map<String, dynamic> json) {
    code = json['Code'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['Code'] = this.code;
    return data;
  }
}