﻿using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Mapper;
using BusinessLogic.Model.Assembly;
using BusinessLogic.Reflection;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;
using FileData;
using FileData.XMLModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XMLUnitTests
{
    [TestClass]
    public class SerializerTest
    {
        private string path = "test.xml";
        private string reflectorPath = @"..\..\..\Tests\LibraryForTests\bin\Debug\LibraryForTests.dll";
        private BaseAssemblyModel assemblyModel = new XMLAssemblyModel();
        [TestMethod]
        public void CheckAmountOfNamespaces()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel,assemblyModel.GetType()),path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));
            Assert.AreEqual(3, model.NamespaceModels.Count);
        }

        [TestMethod]
        public void CheckAmountOfClasses()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> testLibraryTypes =
                model.NamespaceModels.Find(t => t.Name == "TestLibrary").Types;
            List<TypeModel> namespaceTwoTypes = model.NamespaceModels.Find(t => t.Name == "TestLibrary.NamespaceTwo").Types;
            List<TypeModel> namespaceWithRecursionTypes =
                model.NamespaceModels.Find(t => t.Name == "TestLibrary.NamespaceWithRecursion").Types;

            Assert.AreEqual(6, namespaceTwoTypes.Count);
            Assert.AreEqual(3, namespaceWithRecursionTypes.Count);
            Assert.AreEqual(4, testLibraryTypes.Count);
        }

        [TestMethod]
        public void CheckAmountOfStaticClasses()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> staticClasses = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary.NamespaceTwo").Types
                .Where(t => t.Modifiers.StaticEnum == StaticEnum.Static).ToList();

            Assert.AreEqual(1, staticClasses.Count());
        }

        [TestMethod]
        public void CheckAmountOfAbstractClasses()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> abstractClasses = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types
                .Where(t => t.Modifiers.AbstractEnum == AbstractEnum.Abstract).ToList();
            Assert.AreEqual(2, abstractClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithGenericArguments()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> genericClasses = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary.NamespaceTwo").Types.Where(t => t.GenericArguments != null)
                .ToList();
            Assert.AreEqual(1, genericClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfInterfaces()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> interfaces = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Type == TypeEnum.Interface).ToList();
            Assert.AreEqual(1, interfaces.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithBaseType()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classesWithBaseType = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.BaseType != null).ToList();
            Assert.AreEqual(1, classesWithBaseType.Count);
        }

        [TestMethod]
        public void CheckAmountOfPublicClasses()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> publicClasses = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Modifiers.AccessLevel == AccessLevel.Public).ToList();
            Assert.AreEqual(4, publicClasses.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithImplementedInterfaces()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classesWithImplementedInterfaces = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.ImplementedInterfaces.Count > 0).ToList();
            Assert.AreEqual(1, classesWithImplementedInterfaces.Count);
        }

        [TestMethod]
        public void CheckAmountOfClassesWithNestedTypes()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classesWithNestedTypes = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary.NamespaceTwo").Types.Where(t => t.NestedTypes.Count > 0).ToList();
            Assert.AreEqual(2, classesWithNestedTypes.Count);
        }

        [TestMethod]
        public void CheckAmountOfPropertiesInClass()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classes = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(1, classes.First().Properties.Count);
        }

        [TestMethod]
        public void CheckAmountOfMethodsInClass()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classes = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(3, classes.First().Methods.Count);
        }

        [TestMethod]
        public void CheckAmountOfConstructorsInClass()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classes = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(2, classes.First().Constructors.Count);
        }

        [TestMethod]
        public void CheckAmountOfFieldsInClass()
        {
            Reflector reflector = new Reflector(reflectorPath);
            XMLSerializer xmlSerialization = new XMLSerializer();
            xmlSerialization.Save(AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()), path);
            AssemblyModel model = AssemblyModelMapper.MapUp(xmlSerialization.Read(path));

            List<TypeModel> classes = model.NamespaceModels
                .Find(t => t.Name == "TestLibrary").Types.Where(t => t.Name == "PublicClass").ToList();
            Assert.AreEqual(2, classes.First().Fields.Count);
        }
    }
}
