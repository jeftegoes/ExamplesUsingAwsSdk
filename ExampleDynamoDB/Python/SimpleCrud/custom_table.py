class CustomTable:
    def __init__(self, id: str, description: str):
        self.id: str = id
        self.description: str = description

    def to_dict(self) -> dict:
        return {
            'Id': self.id,
            'Description': self.description
        }