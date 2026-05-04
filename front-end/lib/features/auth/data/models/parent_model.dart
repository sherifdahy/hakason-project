import 'package:json_annotation/json_annotation.dart';
import '../../domain/entities/parent_entity.dart';

part 'parent_model.g.dart';

@JsonSerializable()
class ParentModel extends ParentEntity {
  const ParentModel({
    required super.id,
    required super.fullName,
    required super.email,
    required super.token,
  });

  factory ParentModel.fromJson(Map<String, dynamic> json) =>
      _$ParentModelFromJson(json);

  Map<String, dynamic> toJson() => _$ParentModelToJson(this);
}