class KiotError {
  ResponseStatus responseStatus;

  KiotError({this.responseStatus});

  KiotError.fromJson(Map<String, dynamic> json) {
    responseStatus = json['ResponseStatus'] != null
        ? new ResponseStatus.fromJson(json['ResponseStatus'])
        : null;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    if (this.responseStatus != null) {
      data['ResponseStatus'] = this.responseStatus.toJson();
    }
    return data;
  }
}

class ResponseStatus {
  String errorCode;
  String message;

  ResponseStatus({this.errorCode, this.message});

  ResponseStatus.fromJson(Map<String, dynamic> json) {
    errorCode = json['ErrorCode'];
    message = json['Message'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['ErrorCode'] = this.errorCode;
    data['Message'] = this.message;
    return data;
  }
}