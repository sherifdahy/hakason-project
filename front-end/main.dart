import 'package:flutter/material.dart';
import 'package:new_app/core/theme/app_theme.dart';


void main() {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const KidBridgeApp());
}

class KidBridgeApp extends StatelessWidget {
  const KidBridgeApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'KidBridge AI',
      debugShowCheckedModeBanner: false,
      theme: AppTheme.light,
      home: const Scaffold(
        body: Center(child: Text('KidBridge AI')),
      ),
    );
  }
}
