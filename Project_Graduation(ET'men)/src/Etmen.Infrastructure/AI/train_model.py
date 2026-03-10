"""
Offline training script for the Gradient Boosting risk classification model.
Run this script once to produce risk_model_v1.pkl, then convert to ONNX/ZIP for ML.NET.

Usage:
    python train_model.py --data health_data.csv --output risk_model_v1.pkl
"""
from sklearn.ensemble import GradientBoostingClassifier
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report
import joblib
import pandas as pd
import argparse

FEATURES = [
    "Age", "BMI", "BloodPressureSystolic", "BloodPressureDiastolic",
    "BloodSugar", "FamilyHistory", "IsSmoker", "ActivityLevel"
]

def train(data_path: str, output_path: str) -> None:
    df = pd.read_csv(data_path)
    X, y = df[FEATURES], df["IsHighRisk"]

    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

    pipeline = Pipeline([
        ("scaler", StandardScaler()),
        ("model",  GradientBoostingClassifier(
            n_estimators=200, learning_rate=0.05, max_depth=4, random_state=42))
    ])

    pipeline.fit(X_train, y_train)
    print(classification_report(y_test, pipeline.predict(X_test)))
    joblib.dump(pipeline, output_path)
    print(f"Model saved to {output_path}")

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--data",   default="health_data.csv")
    parser.add_argument("--output", default="risk_model_v1.pkl")
    args = parser.parse_args()
    train(args.data, args.output)
