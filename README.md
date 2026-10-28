## Machine Learning Models

The ONNX model files are not included in this repository because of their size and licensing considerations.

### 1. FaceDetection — YuNet

1. Download `face_detection_yunet_2023mar.onnx` from the [OpenCV Zoo repository](https://github.com/opencv/opencv_zoo/tree/main/models/face_detection_yunet).
2. Place it in `FaceDetection/model/face_detection_yunet_2023mar.onnx`.

### 2. FaceEmbedding — W600K-R50

1. Obtain `w600k_r50.onnx` from the appropriate [InsightFace model source](https://github.com/deepinsight/insightface) and review its license terms.
2. Place it in `FaceEmbedding/models/embedding/w600k_r50.onnx`.

The application expects the models at these paths. Make sure the files are present before running the services.

**Note:** Review the applicable model licenses before using these pretrained weights for commercial purposes.