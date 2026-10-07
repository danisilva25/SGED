using System.Text.Json.Nodes;

namespace BNCC.Sync.Agente;

public static class SchemaBncc
{
    public static JsonObject Criar()
    {
        return JsonNode.Parse("""
        {
          "type": "object",
          "properties": {
            "source": {
              "type": "object",
              "properties": {
                "name": {
                  "type": "string"
                },
                "document": {
                  "type": "string"
                },
                "version": {
                  "type": "string"
                },
                "file": {
                  "type": "string"
                }
              },
              "required": [
                "name",
                "document",
                "version",
                "file"
              ]
            },

            "curriculum": {
              "type": "array",
              "items": {
                "type": "object",
                "properties": {

                  "etapa": {
                    "type": "object",
                    "properties": {
                      "nome": {
                        "type": "string"
                      }
                    },
                    "required": [
                      "nome"
                    ]
                  },

                  "areaConhecimento": {
                    "type": "object",
                    "properties": {
                      "nome": {
                        "type": "string"
                      }
                    },
                    "required": [
                      "nome"
                    ]
                  },

                  "componenteCurricular": {
                    "type": "object",
                    "properties": {
                      "nome": {
                        "type": "string"
                      }
                    },
                    "required": [
                      "nome"
                    ]
                  },

                  "organizacao": {
                    "type": "object",
                    "properties": {
                      "tipo": {
                        "type": "string"
                      },
                      "nome": {
                        "type": "string"
                      }
                    },
                    "required": [
                      "tipo",
                      "nome"
                    ]
                  },

                  "estrutura": {
                    "type": "array",
                    "items": {
                      "type": "object",
                      "properties": {

                        "tipo": {
                          "type": "string"
                        },

                        "nome": {
                          "type": "string"
                        },

                        "contexto": {
                          "type": [
                            "object",
                            "null"
                          ],
                          "properties": {
                            "tipo": {
                              "type": "string"
                            },
                            "nome": {
                              "type": "string"
                            }
                          }
                        },

                        "objetosConhecimento": {
                          "type": "array",
                          "items": {
                            "type": "object",
                            "properties": {

                              "nome": {
                                "type": "string"
                              },

                              "habilidades": {
                                "type": "array",
                                "items": {
                                  "type": "object",
                                  "properties": {

                                    "codigo": {
                                      "type": "string"
                                    },

                                    "descricao": {
                                      "type": "string"
                                    },

                                    "anos": {
                                      "type": "array",
                                      "items": {
                                        "type": "integer"
                                      }
                                    },

                                    "fonte": {
                                      "type": "object",
                                      "properties": {
                                        "pagina": {
                                          "type": "integer"
                                        }
                                      },
                                      "required": [
                                        "pagina"
                                      ]
                                    }

                                  },
                                  "required": [
                                    "codigo",
                                    "descricao",
                                    "anos",
                                    "fonte"
                                  ]
                                }
                              }

                            },
                            "required": [
                              "nome",
                              "habilidades"
                            ]
                          }
                        }

                      },
                      "required": [
                        "tipo",
                        "nome",
                        "objetosConhecimento"
                      ]
                    }
                  }

                },
                "required": [
                  "etapa",
                  "areaConhecimento",
                  "componenteCurricular",
                  "organizacao",
                  "estrutura"
                ]
              }
            }
          },

          "required": [
            "source",
            "curriculum"
          ]
        }
        """)!.AsObject();
    }
}